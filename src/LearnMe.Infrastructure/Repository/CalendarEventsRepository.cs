using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Repository.Constants;
using LearnMe.Infrastructure.Repository.Helpers;

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

        // NOT USED
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

        // CALENDAR.CS
        public async Task<IEnumerable<CalendarEvent>> GetFullEventForRoleByFromAndToDateAsync(
            string roleName,
            string userEmail,
            DateTime fromDate,
            DateTime toDate)
        {
            List<CalendarEvent> result = null;

            if (roleName == InfrastructureConstants.StudentRoleName)
            {
                var freeSlots = await _context.CalendarEvents
                    .Where(x => x.Start >= fromDate && x.End <= toDate && x.IsFreeSlot && x.IsDone == false)
                    .Include(x => x.Lesson)
                    .ThenInclude(x => x.UserLessons)
                    .ThenInclude(x => x.User)
                    .AsNoTracking()
                    .ToListAsync();

                var userPlannedLessons = await _context.CalendarEvents
                    .Where(x => x.Start >= fromDate && x.End <= toDate)
                    .Include(x => x.Lesson)
                    .ThenInclude(x => x.UserLessons)
                    .ThenInclude(x => x.User)
                    .Where(x => x.Start >= fromDate && x.End <= toDate
                                                    && x.Lesson.UserLessons.Any(u =>
                                                        u.User.Email.ToLower() == userEmail.ToLower()))
                    .AsNoTracking()
                    .ToListAsync();

                var uniqueResults = freeSlots.Union(userPlannedLessons)
                    .Distinct(new PropertyComparer<CalendarEvent>("CalendarId"));
                result = uniqueResults.ToList();
            }
            else if (roleName == InfrastructureConstants.MentorRoleName
                     || roleName == InfrastructureConstants.AdminRoleName)
            {
                result = await _context.CalendarEvents
                    .Where(x => x.Start >= fromDate && x.End <= toDate)
                    .Include(x => x.Lesson)
                    .ThenInclude(x => x.UserLessons)
                    .ThenInclude(x => x.User)
                    .AsNoTracking()
                    .ToListAsync();
            }

            return result;
        }
        
        // USED
        public async Task<CalendarEvent> InsertFullEventAsync(CalendarEvent fullEvent)
        {
            fullEvent.Lesson.Title ??= "";

            var inserted = await _context.AddAsync<CalendarEvent>(fullEvent);
            bool isSuccess = await SaveAsync();
            bool isSuccess2 = true;

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

                isSuccess2 = await SaveAsync();
            }
            
            CalendarEvent newEvent = null;

            if (isSuccess && isSuccess2)
            {
                newEvent = await GetFullEventByCalendarIdAsync(inserted.Entity.CalendarId);
            }

            return newEvent ?? null;
        }

        // SYNCHRONIZER
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

        // USED
        public async Task<bool> UpdateFullEventByCalendarIdAsync(
            string calendarId,
            CalendarEvent fullEvent)
        {
            var eventToBeUpdated = await GetFullEventByCalendarIdAsync(calendarId);
            if (eventToBeUpdated != null)
            {
                fullEvent.Id = eventToBeUpdated.Id;
                fullEvent.Lesson.Id = eventToBeUpdated.Lesson.Id;

                // Updating user-lessons
                var selectedEmails = new HashSet<string>
                    (fullEvent.Attendees.Select(x => x.Email)).ToArray();

                fullEvent.Lesson.UserLessons = eventToBeUpdated.Lesson.UserLessons;
                
                await UpdateCalendarEventsAttendees(selectedEmails, fullEvent);

                _context.Entry(eventToBeUpdated).State = EntityState.Detached;
                
                return await UpdateAsync(fullEvent);
            }
            else
            {
                return false;
            }
        }

        private async Task UpdateCalendarEventsAttendees(string[] emails, CalendarEvent eventToUpdate)
        {
            if (emails == null)
            {
                eventToUpdate.Lesson.UserLessons = new List<UserLesson>();
                return;
            }

            var emailsNotNull = new HashSet<string>(emails);
            var lessonsUsers = new HashSet<string>
                (eventToUpdate.Lesson.UserLessons.Select(x => x.User.Email));
            foreach (var user in _context.UserBasic.AsNoTracking())
            {
                if (emailsNotNull.Contains(user.Email))
                {
                    if (!lessonsUsers.Contains(user.Email))
                    {
                        eventToUpdate.Lesson.UserLessons.Add(new UserLesson()
                        {
                            LessonId = eventToUpdate.Lesson.Id,
                            UserId = user.Id
                        });
                    }
                } else
                {
                    if (lessonsUsers.Contains(user.Email))
                    {
                        UserLesson userToRemove = eventToUpdate.Lesson.UserLessons
                            .FirstOrDefault(x => x.UserId == user.Id);

                        _context.Remove(userToRemove);
                    }
                }
            }

            await SaveAsync();
        }
    }
}
