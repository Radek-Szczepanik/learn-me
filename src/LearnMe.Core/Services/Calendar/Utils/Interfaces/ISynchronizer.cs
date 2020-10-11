using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface ISynchronizer
    {
        Task<int> SynchronizeDatabaseWithCalendarAsync(
            IExternalCalendarService<Event> externalCalendarService, 
            ICrudRepository<CalendarEvent> repository, 
            string calendarId = Constants.CalendarId);
    }
}
