using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface IExternalCalendarService<T> where T : class
    {
        Task<bool> DeleteEventAsync(string id);

        Task<IEnumerable<T>> GetEventsAsync(
            bool includeCancelled = false,
            string calendarId = Constants.CalendarId);

        Task<T> GetEventByIdAsync(string id);

        Task<T> InsertEventAsync(T obj);

        Task<bool> UpdateEventAsync(T obj);
    }
}
