using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Calendar;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICalendarEventsRepository : ICrudRepository<CalendarEvent>
    {
        Task<bool> DeleteByCalendarIdAsync(string calendarId);

        Task<CalendarEvent> GetByCalendarIdAsync(string calendarId);

        Task<IEnumerable<CalendarEvent>> GetByFromAndToDate(DateTime fromDate, DateTime toDate);

        Task<IEnumerable<CalendarEvent>> GetFullEventByFromAndToDateAsync(DateTime fromDate, DateTime toDate);

        Task<bool> UpdateByCalendarIdAsync(
            string calendarId,
            string summary,
            string description,
            DateTime? startDateTime,
            DateTime? endDateTime);

        Task<CalendarEvent> InsertFullEventAsync(CalendarEvent fullEvent);
    }
}
