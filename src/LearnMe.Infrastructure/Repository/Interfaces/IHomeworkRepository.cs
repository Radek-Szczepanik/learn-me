using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Lessons;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface IHomeworkRepository
    {
        Task<bool> DeleteHomeworkByLessonIdAsync(int lessonId);

        Task<IList<Homework>> GetAllHomeworksByLessonIdAsync(int lessonId);

        Task<Homework> GetHomeworkByFileNameAndLessonIdAsync(int lessonId);

        Task<Homework> InsertHomeworkByLessonIdAsync(Homework homework, int lessonId);
    }
}
