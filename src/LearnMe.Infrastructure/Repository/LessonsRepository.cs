using LearnMe.Infrastructure.Repository.Interfaces;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using System;
using LearnMe.Infrastructure.Models.Domains.Lessons;
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
            var toBeDeleted = await FindLessonByCalendarIdAsync(calendarId);

            if (toBeDeleted != null)
            {
                _context.Remove(toBeDeleted);

                return await SaveAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<Lesson> GetLessonByCalendarIdAsync(string calendarId)
        {
            var found = await FindLessonByCalendarIdAsync(calendarId);
            
            if (found != null)
            {
                _context.Entry(found).State = EntityState.Detached;

                return found;
            }
            else
            {
                return null;
            }
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

        private async Task<Lesson> FindLessonByCalendarIdAsync(string calendarId)
        {
            int? relatedEventDatabaseId = await FindRelatedCalendarEventDatabaseId(calendarId);

            var found = await _context.Lessons
                .AsNoTracking()
                .SingleOrDefaultAsync(l => l.CalendarEventId == relatedEventDatabaseId);

            return found ?? null;
        }
    }
}
