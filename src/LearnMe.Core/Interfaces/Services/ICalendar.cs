using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Services.Calendar.Utils.Constants;
using LearnMe.Shared.Enum.Calendar;

namespace LearnMe.Core.Interfaces.Services
{
    public interface ICalendar
    {
        Task<IEnumerable<CalendarEventDto>> GetAllEventsAsync(
            int eventsPerPage,
            int pageNumber);

        Task<CalendarEventDto> GetEventByIdAsync(int id);

        Task<FullCalendarEventDto> CreateFullEventAsync(
            FullCalendarEventDto eventData,
            string calendarId = CalendarConstants.CalendarId,
            string timezone = CalendarConstants.Timezone,
            bool isRecurringEvent = false,
            Recurrence period = Recurrence.DAILY,
            int recurringEventsCount = 5,
            DateTime? recurUntilDateTime = null,
            IList<string> attendeesEmails = null);

        Task<bool> DeleteEventAsync(int id);
        
        Task<bool> UpdateEventAsync(
            int id,
            CalendarEventDto eventData,
            IList<string> attendeesEmails = null);

        Task<bool> UpdateEventByCalendarIdAsync(
            CalendarEventDto eventData,
            IList<string> attendeesEmails = null);

        Task<bool> UpdateFullEventByCalendarIdAsync(
            FullCalendarEventDto eventData,
            IList<string> attendeesEmails = null);

        Task<bool> DeleteEventByCalendarIdAsync(string calendarId);

        Task<bool> DeleteFullEventByCalendarIdAsync(string calendarId);

        Task<IEnumerable<CalendarEventDto>> GetEventsByDatesAsync(
            DateTime fromDate,
            DateTime toDate);

        Task<IEnumerable<FullCalendarEventDto>> GetFullEventsByDatesAsync(
            DateTime fromDate,
            DateTime toDate);

        Task<IEnumerable<FullCalendarEventDto>> GetFullEventsByUserRoleByDatesAsync(
            string roleName,
            string userEmail,
            DateTime fromDate,
            DateTime toDate);
    }
}
