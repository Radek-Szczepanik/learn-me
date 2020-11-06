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

        public async Task<CalendarEvent> GetByCalendarIdAsync(string calendarId)
        {
            var result = await _context.CalendarEvents
                                                   .Where(o => o.CalendarId == calendarId)
                                                   .SingleOrDefaultAsync();
            
            _context.Entry(result).State = EntityState.Detached;

            return result;
        }

        public async Task<IEnumerable<CalendarEvent>> GetByFromAndToDate(DateTime fromDate, DateTime toDate)
        {
            var result = await _context.CalendarEvents
                                                        .Where(o => o.Start >= fromDate && o.End <= toDate)
                                                        .AsNoTracking()
                                                        .ToListAsync();

            return result;
        }
    }
}
