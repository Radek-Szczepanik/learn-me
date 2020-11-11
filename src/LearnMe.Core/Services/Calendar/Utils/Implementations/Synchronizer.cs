using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class Synchronizer : ISynchronizer
    {
        public async Task<int> SynchronizeDatabaseWithCalendarByDateModifiedAsync(
            IExternalCalendarService<Event> externalCalendarService,
            ICalendarEventsRepository repository,
            ICrudRepository<CalendarSynchronization> synchronizationData,
            int lastSynchronizationId = Constants.LastSynchronizationRecordId,
            string calendarId = Constants.CalendarId)
        {
            int synchronizedRowsCounter = 0;
            var lastSynchronization = await synchronizationData.GetByIdAsync(lastSynchronizationId);

            IEnumerable<Event> eventsFromExternalCalendarToBeSynchronized = 
                await externalCalendarService.GetEventsByLastUpdateAsync(lastSynchronization.LastSynchronization, true);
            var updateDateTime = DateTime.UtcNow;

            IList<string> databaseCalendarIds = await Helpers.GetListOfCalendarIdsFromDatabase(repository);

            foreach (var eventResult in eventsFromExternalCalendarToBeSynchronized)
            {
                // TODO: Add to Calendar specific log
                if (IsInDatabase(databaseCalendarIds, eventResult.Id) && eventResult.Status == "cancelled")
                {
                    await repository.DeleteByCalendarIdAsync(eventResult.Id);
                }
                else if (IsInDatabase(databaseCalendarIds, eventResult.Id) && eventResult.Status != "cancelled")
                {
                    await repository.UpdateByCalendarIdAsync(
                        eventResult.Id,
                        eventResult.Summary,
                        eventResult.Description,
                        eventResult.Start.DateTime,
                        eventResult.End.DateTime);
                }
                else if (!IsInDatabase(databaseCalendarIds, eventResult.Id) && eventResult.Status != "cancelled"
                                                                            && HasNotBeenCreatedAndDeletedBeforeSynchronization(eventResult))
                {
                    await repository.InsertAsync(new CalendarEvent()
                    {
                        Title = eventResult.Summary,
                        Description = eventResult.Description,
                        Start = eventResult.Start.DateTime,
                        End = eventResult.End.DateTime,
                        IsDone = false,
                        IsFreeSlot = false,
                        CalendarId = eventResult.Id
                    });
                }
                synchronizedRowsCounter++;
            }

            if (SuccessfulSynchronization(eventsFromExternalCalendarToBeSynchronized, synchronizedRowsCounter))
            {
                await synchronizationData.UpdateAsync(new CalendarSynchronization()
                {
                    Id = Constants.LastSynchronizationRecordId,
                    LastSynchronization = updateDateTime
                });
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

        private bool SuccessfulSynchronization(IEnumerable<Event> eventsToSynchronize, int numberOfSynchronizedItems)
        {
            return (eventsToSynchronize.Count() != 0 && numberOfSynchronizedItems > 0);
        }
    }
}