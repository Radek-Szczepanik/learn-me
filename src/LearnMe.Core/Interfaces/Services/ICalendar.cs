using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Services.Calendar.Utils;
using LearnMe.Shared.Enum.Calendar;

namespace LearnMe.Core.Interfaces.Services
{
    public interface ICalendar
    {
        Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(
            int eventsPerPage,
            int pageNumber,
            string calendarId = Constants.CalendarId);

        Task<CalendarEventDto> GetEventByIdAsync(int id);

        Task<CalendarEventDto> CreateEventAsync(
            CalendarEventDto eventData,
            string calendarId = Constants.CalendarId,
            string timezone = Constants.Timezone,
            bool isRecurringEvent = false,
            Recurrence period = Recurrence.DAILY,
            int recurringEventsCount = 5,
            DateTime? recurUntilDateTime = null);

        Task<bool> UpdateEventAsync(int id,  CalendarEventDto eventData);

        Task<bool> DeleteEventAsync(int id);

        Task<bool> UpdateEventByCalendarIdAsync(CalendarEventDto eventData);

        Task<bool> DeleteEventByCalendarIdAsync(string calendarId);
    }
}
