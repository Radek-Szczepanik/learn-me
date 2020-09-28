using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface IGoogleCRUD
    {
        Task<IEnumerable<Event>> GetAllEventsAsync(
            CalendarService calendarService,
            bool includeCancelled = false,
            string calendarId = Constants.CalendarId,
            int maxNumberOfResults = 250,
            string nextPageToken = null);
    }
}
