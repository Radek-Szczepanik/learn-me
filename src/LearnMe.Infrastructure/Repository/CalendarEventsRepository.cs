using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using LearnMe.Infrastructure.Models.Domains.Lessons;

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
            //await _context.CalendarEvents
            //    .Include(x => x.Lesson)
            //    .ThenInclude(x => x.UserLessons)
            //    .Single(x => x.CalendarId == calendarId)

            return await DeleteAsync(
                _context.CalendarEvents
                    .Include(x => x.Lesson)
                    .ThenInclude(x => x.UserLessons)
                    .Single(x => x.CalendarId == calendarId));

            // OK
            //return await DeleteAsync(
            //    _context.CalendarEvents
            //        .Single(x => x.CalendarId == calendarId));
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

        public async Task<CalendarEvent> GetFullEventByCalendarIdAsync(string calendarId)
        {
            var result = await _context.CalendarEvents
                .Where(x => x.CalendarId == calendarId)
                .Include(x => x.Lesson)
                .ThenInclude(x => x.UserLessons)
                .ThenInclude(x => x.User)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<CalendarEvent>> GetFullEventByFromAndToDateAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _context.CalendarEvents
                .Where(x => x.Start >= fromDate && x.End <= toDate)
                .Include(x => x.Lesson)
                .ThenInclude(x => x.UserLessons)
                .ThenInclude(x => x.User)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        // USED
        public async Task<CalendarEvent> InsertFullEventAsync(CalendarEvent fullEvent)
        {
            var inserted = await _context.AddAsync<CalendarEvent>(fullEvent);
            bool isSuccess = await SaveAsync();

            if (fullEvent.Attendees.Count != 0)
            {
                foreach (var person in fullEvent.Attendees)
                {
                    var user = await _context.UserBasic
                        .Where(x => x.Email == person.Email)
                        .AsNoTracking()
                        .SingleAsync();

                    var userLesson = new UserLesson()
                    {
                        LessonId = inserted.Entity.Lesson.Id,
                        UserId = user.Id
                    };

                    var updated = await _context.UserLessons.AddAsync(userLesson);
                }
            }
            bool isSuccess2 = await SaveAsync();

            CalendarEvent newEvent = null;

            if (isSuccess && isSuccess2)
            {
                newEvent = await GetFullEventByCalendarIdAsync(inserted.Entity.CalendarId);
            }

            return newEvent ?? null;
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

        public async Task<bool> UpdateFullEventByCalendarIdAsync(
            string calendarId,
            CalendarEvent fullEvent)
        {
            //var eventToBeUpdated = await GetFullEventByCalendarIdAsync(calendarId);

            //fullEvent.Id = eventToBeUpdated.Id;

            await UpdateAsync(fullEvent);

            return await SaveAsync();
            //_context.CalendarEvents.Update(fullEvent);

            //return await _context.SaveChangesAsync();
        }
    }
}
