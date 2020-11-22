using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.Services.Calendar.Utils.Constants;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface ISynchronizer
    {
        Task<int> SynchronizeDatabaseWithCalendarByDateModifiedAsync(
            IExternalCalendarService<Event> externalCalendarService,
            ICalendarEventsRepository repository,
            ICrudRepository<CalendarSynchronization> synchronizationData,
            int lastSynchronizationId = ApplicationConstants.LastSynchronizationRecordId,
            string calendarId = CalendarConstants.CalendarId);
    }
}
