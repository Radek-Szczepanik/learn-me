using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace LearnMe.Infrastructure.Repository
{
    public class CalendarEventsRepository : CrudRepository<CalendarEvent>, ICalendarEventsRepository
    {
        private readonly ApplicationDbContext _context;

        public CalendarEventsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByCalendarIdAsync(string calendarId)
        {
            return await DeleteAsync(
                _context.CalendarEvents
                    .Single(x => x.CalendarId == calendarId));
        }

        public Task<CalendarEvent> GetByCalendarIdAsync(string calendarId)
        {
            return _context.CalendarEvents
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.CalendarId == calendarId);
        }

        public async Task<IEnumerable<CalendarEvent>> GetByFromAndToDate(DateTime fromDate, DateTime toDate)
        {
            var result = await _context.CalendarEvents
                                                        .Where(x => x.Start >= fromDate && x.End <= toDate)
                                                        .AsNoTracking()
                                                        .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateByCalendarIdAsync(
            string calendarId,
            string summary,
            string description,
            DateTime? startDateTime,
            DateTime? endDateTime)
        {
            var eventToBeUpdated = await GetByCalendarIdAsync(calendarId);

            return await UpdateAsync(new CalendarEvent()
            {
                Id = eventToBeUpdated.Id,
                Title = summary,
                Description = description,
                Start = startDateTime,
                End = endDateTime,
                IsDone = eventToBeUpdated.IsDone,
                IsFreeSlot = eventToBeUpdated.IsFreeSlot,
                CalendarId = calendarId
            });
        }
    }
}
