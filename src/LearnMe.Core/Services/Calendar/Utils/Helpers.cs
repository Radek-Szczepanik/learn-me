using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils
{
    public static class Helpers
    {
        public static IList<string> GetListOfCalendarIdsFromDatabase(ICrudRepository<CalendarEvent> repository)
        {
            var allDatabaseEvents = repository.GetAllAsync().Result;
            
            IList<string> databaseEventsCalendarIds = new List<string>();
            foreach (var eventFromDatabase in allDatabaseEvents)
            {
                databaseEventsCalendarIds.Add(eventFromDatabase.CalendarId);
            }

            return databaseEventsCalendarIds;
        }
    }
}
