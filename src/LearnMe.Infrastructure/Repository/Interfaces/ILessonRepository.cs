using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Lessons;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ILessonsRepository
    {
        Task<Lesson> CreateLessonAsync(string calendarId, Lesson lesson);

        Task<bool> DeleteLessonByCalendarIdAsync(string calendarId);

        Task<Lesson> GetLessonByCalendarIdAsync(string calendarId);

        Task<bool> UpdateLessonByCalendarIdAsync(string calendarId, Lesson lesson);
    }
}
