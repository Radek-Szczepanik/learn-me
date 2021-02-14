using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Lessons;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface IHomeworkRepository : ICrudRepository<Homework>
    {
        Task<bool> DeleteHomeworkByLessonIdAsync(int lessonId);

        Task<IList<Homework>> GetAllHomeworksByLessonIdAsync(int lessonId, string userId = null);

        Task<IList<Homework>> GetAllHomeworksTypeDoneByLessonIdAsync(int lessonId);

        Task<Homework> GetHomeworkByFileNameAndLessonIdAsync(int lessonId);

        Task<Homework> InsertHomeworkByLessonIdAsync(Homework homework, int lessonId, string userId = "");
    }
}
