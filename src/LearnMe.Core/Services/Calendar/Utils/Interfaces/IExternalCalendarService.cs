using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface IExternalCalendarService<T> where T : class
    {
        Task<bool> DeleteEventAsync(string id);

        Task<IEnumerable<T>> GetEventsAsync(bool includeCancelled = false);

        Task<IEnumerable<T>> GetEventsByLastUpdateAsync(
            DateTime? updateDateTime,
            bool includeCancelled = false);

        Task<T> GetEventByIdAsync(string id);

        Task<T> InsertEventAsync(T obj);

        Task<bool> UpdateEventAsync(string id, T obj);
    }
}
