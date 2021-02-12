using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Invoice;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnMe.Infrastructure.Repository
{
    public class HomeworkRepository : CrudRepository<Homework>, IHomeworkRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeworkRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteHomeworkByLessonIdAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Homework>> GetAllHomeworksByLessonIdAsync(int lessonId)
        {
            var lesson = await _context.Lessons
                .Where(x => x.Id == lessonId)
                .Include(x => x.UserLessons)
                .ThenInclude(x => x.Homeworks)
                .SingleOrDefaultAsync();

            if (lesson != null)
            {
                // TODO: Refactor the below
                // Returns list of homeworks for the 1st UserLesson = since all of the UserLessons
                // have assigned the full list of homeworks from one lesson

                return lesson.UserLessons[1].Homeworks;
            }

            return new List<Homework>();
        }

        public async Task<Homework> GetHomeworkByFileNameAndLessonIdAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        public async Task<Homework> InsertHomeworkByLessonIdAsync(Homework homework, int lessonId)
        {
            homework.UserLessonHomeworkList ??= new List<UserLessonHomework>();
            var result = await _context.Homeworks.AddAsync(homework);
            await _context.SaveChangesAsync();

            var homeworkGet = await _context.Homeworks
                .Where(x => x.FileString == homework.FileString
                 && x.MessageText == homework.MessageText)
                .SingleOrDefaultAsync();

            var lesson = await _context.Lessons
                .Where(x => x.Id == lessonId)
                .Include(x => x.UserLessons)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            foreach (var student in lesson.UserLessons)
            {
                await _context.UserLessonHomeworks.AddAsync(new UserLessonHomework()
                {
                    UserLessonId = student.Id,
                    HomeworkId = homeworkGet.Id,
                    CorrectionId = null
                });

                await _context.SaveChangesAsync();
            }

            return homeworkGet;
        }
    }
}
