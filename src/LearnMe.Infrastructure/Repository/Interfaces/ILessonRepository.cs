using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ILessonsRepository
    {
        Task<Lesson> CreateLessonAsync(string calendarId, Lesson lesson);

        Task<bool> DeleteLessonByCalendarIdAsync(string calendarId);

        Task<Lesson> GetLessonByCalendarIdAsync(string calendarId);

        Task<bool> UpdateLessonByCalendarIdAsync(string calendarId, Lesson lesson);

        Task<IList<UserBasic>> GetLessonAttendeesAsync(Lesson lesson);

        Task<UserBasic> CreateLessonAttendeeAsync(Lesson lesson, string attendeeEmail);

        Task<UserBasic> DeleteLessonAttendeeAsync(Lesson lesson, string attendeeEmail);
    }
}
