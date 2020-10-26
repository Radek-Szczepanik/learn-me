using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Calendar;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICalendarEventsRepository : ICrudRepository<CalendarEvent>
    {
        Task<CalendarEvent> GetByCalendarIdAsync(string calendarId);
    }
}
