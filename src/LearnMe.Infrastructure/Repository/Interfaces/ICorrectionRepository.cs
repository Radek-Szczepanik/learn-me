using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Lessons;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICorrectionRepository
    {
        Task<bool> DeleteCorrectionByHomeworkIdAsync(int lessonId);

        Task<IList<Homework>> GetAllCorrectionByHomeworkIdAsync(int lessonId);

        Task<Homework> GetCorrectionByHomeworkIdAsync(int lessonId);

        Task<Homework> InsertCorrectionByHomeworkIdAsync(Homework homework, int lessonId);
    }
}
