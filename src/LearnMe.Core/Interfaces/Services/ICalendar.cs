using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Core.DTO.Calendar;

namespace LearnMe.Core.Interfaces.Services
{
    public interface ICalendar
    {
        Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(string calendarId = "primary");

        Task<CalendarEventDto> GetEventByIdAsync(int id);

        Task<bool> CreateEventAsync(CalendarEventDto eventData, string calendarId = "primary", string timezone = "Europe/Warsaw", bool isRecurringEvent = false);

        Task<bool> UpdateEventAsync(int id,  CalendarEventDto eventData);

        Task<bool> DeleteEventAsync(int id);
    }
}
