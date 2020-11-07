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
        public async Task<int> SynchronizeDatabaseWithCalendarByIdAsync(
            IExternalCalendarService<Event> externalCalendarService,
            ICrudRepository<CalendarEvent> repository,
            string calendarId = Constants.CalendarId)
        {
            int synchronizedRowsCounter = 0;

            IEnumerable<Event> eventsFromCalendarResult = await externalCalendarService.GetEventsAsync();

            IList<string> databaseCalendarIds = await Helpers.GetListOfCalendarIdsFromDatabase(repository);

            foreach (var eventResult in eventsFromCalendarResult)
            {
                // TODO: Add to Calendar specific log
                if (!databaseCalendarIds.Contains(eventResult.Id) && eventResult.Status != "cancelled")
                {
                    await repository.InsertAsync(new CalendarEvent()
                    {
                        Title = eventResult.Summary,
                        Description = eventResult.Description,
                        Start = eventResult.Start.DateTime,
                        End = eventResult.End.DateTime,
                        IsDone = false,
                        CalendarId = eventResult.Id
                    });

                    synchronizedRowsCounter++;
                }
            }

            return synchronizedRowsCounter;
        }

        public async Task<int> SynchronizeDatabaseWithCalendarByDateModifiedAsync(
            IExternalCalendarService<Event> externalCalendarService,
            ICalendarEventsRepository repository,
            ICrudRepository<CalendarSynchronization> synchronizationData,
            int lastSynchronizationId = Constants.LastSynchronizationRecordId,
            string calendarId = Constants.CalendarId)
        {
            int synchronizedRowsCounter = 0;
            var lastSynchronization = await synchronizationData.GetByIdAsync(lastSynchronizationId);

            IEnumerable<Event> eventsFromCalendarResult = await externalCalendarService.GetEventsByLastUpdateAsync(lastSynchronization.LastSynchronization);
            var updateDateTime = DateTime.Now;

            IList<string> databaseCalendarIds = await Helpers.GetListOfCalendarIdsFromDatabase(repository);

            foreach (var eventResult in eventsFromCalendarResult)
            {
                // TODO: Add to Calendar specific log
                if (!databaseCalendarIds.Any(e => e == eventResult.Id) && eventResult.Status != "cancelled")
                {
                    await repository.InsertAsync(new CalendarEvent()
                    {
                        Title = eventResult.Summary,
                        Description = eventResult.Description,
                        Start = eventResult.Start.DateTime,
                        End = eventResult.End.DateTime,
                        IsDone = false,
                        CalendarId = eventResult.Id
                    });

                    synchronizedRowsCounter++;
                }
                else if (eventResult.Status == "cancelled")
                {
                    if (eventResult.Start == null || eventResult.End == null)
                    {
                        // do nothing - means one from the recurring events has been deleted from external calendar
                    }
                    else
                    {
                        var eventToBeDeleted =
                            await repository.GetByCalendarIdAsync(eventResult.Id);
                        
                        await repository.DeleteAsync(eventToBeDeleted.Id);
                    }
                }
                else
                {
                    await repository.UpdateAsync(new CalendarEvent()
                    {
                        Title = eventResult.Summary,
                        Description = eventResult.Description,
                        Start = eventResult.Start.DateTime,
                        End = eventResult.End.DateTime,
                        IsDone = false, // TODO refactor it
                        CalendarId = eventResult.Id
                    });
                }
            }

            await synchronizationData.UpdateAsync(new CalendarSynchronization()
            {
                Id = Constants.LastSynchronizationRecordId,
                LastSynchronization = DateTime.Now
            });

            return synchronizedRowsCounter;
        }
    }
}