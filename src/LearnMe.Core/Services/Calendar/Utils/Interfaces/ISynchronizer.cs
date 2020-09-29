using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface ISynchronizer
    {
        Task<int> SynchronizeDatabaseWithCalendarAsync(
            IGoogleCRUD googleCalendarAccess, 
            CalendarService calendarService, 
            ICrudRepository<CalendarEvent> repository, 
            string calendarId = Constants.CalendarId);
    }
}
