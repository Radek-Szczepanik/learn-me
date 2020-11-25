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

        public async Task<IList<UserBasic>> GetLessonAttendees(Lesson lesson)
        {
            IList<UserBasic> result = new List<UserBasic>();

            var fullLesson = await _context.Lessons
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
