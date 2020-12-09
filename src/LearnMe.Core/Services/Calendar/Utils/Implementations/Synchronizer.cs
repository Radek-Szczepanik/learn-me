using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Constants;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class Synchronizer : ISynchronizer
    {
        private readonly IDate _datetime;

        public Synchronizer(IDate datetime)
        {
            _datetime = datetime;
        }

        public async Task<int> SynchronizeDatabaseWithCalendarByDateModifiedAsync(
            IExternalCalendarService<Event> externalCalendarService,
            ICalendarEventsRepository repository,
            ILessonsRepository lessonsRepository,
            ICrudRepository<CalendarSynchronization> synchronizationData,
            ICrudRepository<CalendarEvent> eventsData,
            int lastSynchronizationId = ApplicationConstants.LastSynchronizationRecordId,
            string calendarId = CalendarConstants.CalendarId)
        {
            int synchronizedRowsCounter = 0;
            var lastSynchronizationDateTime = await GetLastSynchronizationDateFromDatabase(synchronizationData, lastSynchronizationId);

            IEnumerable<Event> eventsFromExternalCalendarToBeSynchronized = 
                await externalCalendarService.GetEventsByLastUpdateAsync(lastSynchronizationDateTime, true);
            var updateDateTime = _datetime.Now;

            IList<string> databaseCalendarIds = await Helpers.GetListOfCalendarIdsFromDatabase(repository);

            foreach (var eventResult in eventsFromExternalCalendarToBeSynchronized)
            {
                // TODO: Add to Calendar specific log
                if (IsInDatabase(databaseCalendarIds, eventResult.Id) && eventResult.Status == CalendarConstants.CancelledEventStatus)
                {
                    await repository.DeleteByCalendarIdAsync(eventResult.Id);
                }
                else if (IsInDatabase(databaseCalendarIds, eventResult.Id) && eventResult.Status != CalendarConstants.CancelledEventStatus)
                {
                    await repository.UpdateByCalendarIdAsync(
                        eventResult.Id,
                        eventResult.Summary,
                        eventResult.Description,
                        eventResult.Start.DateTime,
                        eventResult.End.DateTime);
                    
                    // Update Event=Lesson attendees
                    IList<string> attendeesEmailsToSynchronize = GetAttendeesEmailsToSynchronize(eventResult);
                    var lessonToUpdate = await lessonsRepository.GetLessonByCalendarIdAsync(eventResult.Id);
                    var lessonAttendeesFromDb = await lessonsRepository.GetLessonAttendeesAsync(lessonToUpdate);

                    IList<string> attendeesEmailsFromDb = GetAttendeesEmailsFromDb(lessonAttendeesFromDb);

                    var emailsToAddToDb = attendeesEmailsToSynchronize.Except(attendeesEmailsFromDb).ToList();
                    var emailsToDeleteFromDb = attendeesEmailsFromDb.Except(attendeesEmailsToSynchronize).ToList();

                    foreach (var item in emailsToAddToDb)
                    {
                        await lessonsRepository.CreateLessonAttendeeAsync(lessonToUpdate, item);
                    }

                    foreach (var item in emailsToDeleteFromDb)
                    {
                        await lessonsRepository.DeleteLessonAttendeeAsync(lessonToUpdate, item);
                    }
                }
                else if (!IsInDatabase(databaseCalendarIds, eventResult.Id)
                         && eventResult.Status != CalendarConstants.CancelledEventStatus
                         && HasNotBeenCreatedAndDeletedBeforeSynchronization(eventResult))
                {
                    var newDbEvent = await repository.InsertAsync(new CalendarEvent()
                    {
                        Title = eventResult.Summary,
                        Description = eventResult.Description,
                        Start = eventResult.Start.DateTime,
                        End = eventResult.End.DateTime,
                        IsDone = false,
                        IsFreeSlot = false,
                        CalendarId = eventResult.Id
                    });

                    var newDbLesson = await lessonsRepository.CreateLessonAsync(eventResult.Id, new Lesson()
                    {
                        CalendarEventId = newDbEvent.Id,
                        LessonStatus = 0,
                        Title = eventResult.Summary,
                        RelatedInvoiceId = null
                    });

                    //lessonsRepository._context.Entry(found).State = EntityState.Detached;

                    // Insert Event=Lesson attendees
                    IList<string> attendeesEmailsToSynchronize = GetAttendeesEmailsToSynchronize(eventResult);

                    foreach (var item in attendeesEmailsToSynchronize)
                    {
                        await lessonsRepository.CreateLessonAttendeeAsync(newDbLesson, item);
                    }
                }
                synchronizedRowsCounter++;
            }

            if (IsSuccessfulSynchronization(eventsFromExternalCalendarToBeSynchronized, synchronizedRowsCounter))
            {
                await UpdateLastSynchronizationDateInDatabase(synchronizationData, updateDateTime);
            }

            return synchronizedRowsCounter;
        }

        private bool IsInDatabase(IList<string> databaseIds, string id)
        {
            return databaseIds.Any(i => i == id);
        }

        private bool HasNotBeenCreatedAndDeletedBeforeSynchronization(Event calendarEvent)
        {
            return (calendarEvent.Start != null || calendarEvent.End != null);
        }

        private bool IsSuccessfulSynchronization(IEnumerable<Event> eventsToSynchronize, int numberOfSynchronizedItems)
        {
            return (eventsToSynchronize.Count() == numberOfSynchronizedItems);
        }

        private async Task<DateTime?> GetLastSynchronizationDateFromDatabase(
            ICrudRepository<CalendarSynchronization> databaseTable,
            int lastSynchronizationId)
        {
            var lastSynchronizationRecord = await databaseTable.GetByIdAsync(lastSynchronizationId);

            return lastSynchronizationRecord.LastSynchronization;
        }

        private async Task<bool> UpdateLastSynchronizationDateInDatabase(ICrudRepository<CalendarSynchronization> tableToUpdate, DateTime dateTime)
        {
            return await tableToUpdate.UpdateAsync(new CalendarSynchronization()
            {
                Id = ApplicationConstants.LastSynchronizationRecordId,
                LastSynchronization = dateTime
            });
        }

        private IList<string> GetAttendeesEmailsToSynchronize(Event eventFromCalendar)
        {
            IList<string> emails = new List<string>();

            if (eventFromCalendar.Attendees != null && eventFromCalendar.Attendees.Count != 0)
            {
                foreach (var item in eventFromCalendar.Attendees)
                {
                    emails.Add(item.Email);
                }
            }

            return emails;
        }

        private IList<string> GetAttendeesEmailsFromDb(IList<UserBasic> attendeesFromDb)
        {
            IList<string> emails = new List<string>();

            if (attendeesFromDb.Count != 0)
            {
                foreach (var item in attendeesFromDb)
                {
                    emails.Add(item.Email);
                }
            }

            return emails;
        }
    }
}