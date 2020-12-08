using LearnMe.Infrastructure.Repository.Interfaces;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.EntityFrameworkCore;

namespace LearnMe.Infrastructure.Repository
{
    public class LessonsRepository : CrudRepository<Lesson>, ILessonsRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly ICalendarEventsRepository _calendarEventsRepository;

        public LessonsRepository(
            ApplicationDbContext context,
            ICalendarEventsRepository calendarEventsRepository) : base(context)
        {
            _context = context;
            _calendarEventsRepository = calendarEventsRepository;
        }

        public async Task<Lesson> CreateLessonAsync(string calendarId, Lesson lesson)
        {
            int? relatedEventDatabaseId = await FindRelatedCalendarEventDatabaseId(calendarId);

            if (relatedEventDatabaseId != null)
            {
                lesson.CalendarEventId = (int)relatedEventDatabaseId;

                return await InsertAsync(lesson);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteLessonByCalendarIdAsync(string calendarId)
        {
            int? relatedEventDatabaseId = await FindRelatedCalendarEventDatabaseId(calendarId);

            _context.Remove(_context.Lessons
                .AsNoTracking()
                .SingleOrDefaultAsync(l => l.CalendarEventId == relatedEventDatabaseId));

            return await SaveAsync();
        }

        public async Task<Lesson> GetLessonByCalendarIdAsync(string calendarId)
        {
            int? relatedEventDatabaseId = await FindRelatedCalendarEventDatabaseId(calendarId);

            var found = await _context.Lessons
                .AsNoTracking()
                .SingleOrDefaultAsync(l => l.CalendarEventId == relatedEventDatabaseId);

            return found ?? null;
        }

        public async Task<bool> UpdateLessonByCalendarIdAsync(string calendarId, Lesson lesson)
        {
            //int? relatedEventDatabaseId = await FindRelatedCalendarEventDatabaseId(calendarId);
            var lessonOldRecord = await GetLessonByCalendarIdAsync(calendarId);

            if (lessonOldRecord != null)
            {
                lesson.Id = lessonOldRecord.Id;
                lesson.CalendarEventId = lessonOldRecord.CalendarEventId;

                return await UpdateAsync(lesson);
            }
            else
            {
                return false;
            }
        }

        public async Task<IList<UserBasic>> GetLessonAttendeesAsync(Lesson lesson)
        {
            IList<UserBasic> result = new List<UserBasic>();

            var fullLesson = await _context.Lessons
                .AsNoTracking()
                .Where(x => x.Id == lesson.Id)
                .Include(x => x.UserLessons)
                .ThenInclude(x => x.User)
                .SingleOrDefaultAsync();

            foreach (var item in fullLesson.UserLessons)
            {
                result.Add(item.User);
            }

            return result;
        }

        public async Task<UserBasic> CreateLessonAttendeeAsync(Lesson lesson, string attendeeEmail)
        {
            try
            {
                _context.Entry(lesson).State = EntityState.Detached;

                var fullLesson = await _context.Lessons
                    .Where(x => x.Id == lesson.Id)
                    .Include(x => x.UserLessons)
                    .ThenInclude(x => x.User)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

                var userBasic = await _context.UserBasic
                    .Where(x => x.Email == attendeeEmail)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

                _context.Entry(userBasic).State = EntityState.Detached;

                var newUserLesson = new UserLesson()
                {
                    Lesson = lesson,
                    User = userBasic
                };
                _context.Entry(fullLesson).State = EntityState.Detached;
                _context.Entry(userBasic).State = EntityState.Detached;
                _context.Entry(newUserLesson).State = EntityState.Detached;
                fullLesson.UserLessons.Add(newUserLesson);
                // TODO: Fix error when adding new Calendar Event - ok, then new Lesson - ok, then Lesson Id is apparently already tracked so Exception here - cannot add Users to synchronized new lesson
                _context.Lessons
                    .Update(fullLesson);

                await _context.SaveChangesAsync();

                _context.Entry(fullLesson).State = EntityState.Detached;
                _context.Entry(userBasic).State = EntityState.Detached;
                _context.Entry(newUserLesson).State = EntityState.Detached;

                return userBasic;
            }
            catch (Exception ex)
            {
                _context.Entry(lesson).State = EntityState.Detached;
                
                //var fullLesson = await _context.Lessons
                //    .AsNoTracking()
                //    .Where(x => x.Id == lesson.Id)
                //    .Include(x => x.UserLessons)
                //    .ThenInclude(x => x.User)
                //    .SingleOrDefaultAsync();

                var fullLesson = await _context.Lessons
                    .Where(x => x.Id == lesson.Id)
                    .Include(x => x.UserLessons)
                    .ThenInclude(x => x.User)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();


                var userBasic = await _context.UserBasic
                    .Where(x => x.Email == attendeeEmail)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
                
                _context.Entry(userBasic).State = EntityState.Detached;

                var newUserLesson = new UserLesson()
                {
                    Lesson = lesson,
                    User = userBasic
                };

                fullLesson.UserLessons.Add(newUserLesson);
                _context.Entry(fullLesson).State = EntityState.Detached;
                // Fix applied
                // TODO: Fix works only for adding one UserLesson when synchronizing from google
                _context.Lessons
                    .Update(fullLesson);

                await _context.SaveChangesAsync();
                _context.Entry(fullLesson).State = EntityState.Detached;
                _context.Entry(userBasic).State = EntityState.Detached;
                return userBasic;
                //return null;
            }
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }

        public async Task<UserBasic> DeleteLessonAttendeeAsync(Lesson lesson, string attendeeEmail)
        {
            try
            {
                var userBasic = await _context.UserBasic
                    .AsNoTracking()
                    .Where(x => x.Email == attendeeEmail)
                    .SingleOrDefaultAsync();

                var userLesson = await _context.UserLessons
                    .AsNoTracking()
                    .Where(x => x.User == userBasic && x.LessonId == lesson.Id)
                    .SingleOrDefaultAsync();

                _context.UserLessons.Remove(userLesson);

                await _context.SaveChangesAsync();

                return userBasic;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<int?> FindRelatedCalendarEventDatabaseId(string calendarId)
        {
            var calendarEventRelatedToLesson = await _calendarEventsRepository.GetByCalendarIdAsync(calendarId);

            if (calendarEventRelatedToLesson != null)
            {
                return calendarEventRelatedToLesson.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
