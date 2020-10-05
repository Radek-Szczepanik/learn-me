using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;


namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class Synchronizer : ISynchronizer
    {
        public async Task<int> SynchronizeDatabaseWithCalendarAsync(
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
    }
}