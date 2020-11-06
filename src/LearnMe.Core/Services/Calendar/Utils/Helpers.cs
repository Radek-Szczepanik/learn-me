using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils
{
    public static class Helpers
    {
        public static async Task<IList<string>> GetListOfCalendarIdsFromDatabase(
            ICrudRepository<CalendarEvent> repository,
            int eventsPerPage = 250,
            int pageNumber = 1)
        {
            var allDatabaseEvents = await repository.GetAllWithPagination(eventsPerPage, pageNumber);
            
            IList<string> databaseEventsCalendarIds = new List<string>();
            foreach (var eventFromDatabase in allDatabaseEvents)
            {
                databaseEventsCalendarIds.Add(eventFromDatabase.CalendarId);
            }

            return databaseEventsCalendarIds;
        }
    }
}
