using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Services.Calendar.Utils;

namespace LearnMe.Core.Interfaces.Services
{
    public interface ICalendar
    {
        Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(string calendarId = Constants.CalendarId);

        Task<CalendarEventDto> GetEventByIdAsync(int id);

        Task<bool> CreateEventAsync(
            CalendarEventDto eventData,
            string calendarId = Constants.CalendarId,
            string timezone = Constants.Timezone,
            bool isRecurringEvent = false);

        Task<bool> UpdateEventAsync(int id,  CalendarEventDto eventData);

        Task<bool> DeleteEventAsync(int id);
    }
}
