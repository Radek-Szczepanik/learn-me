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
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (lesson != null)
            {
                var result = new List<Homework>();
                // TODO: Refactor the below
                foreach (var userLesson in lesson.UserLessons)
                {
                    var listOfUserLessonHomeworks = await _context.UserLessonHomeworks
                        .Where(x => x.UserLesson == userLesson)
                        .Include(x => x.Homework)
                        .AsNoTracking()
                        .ToListAsync();

                    listOfUserLessonHomeworks
                        .Where(x => result
                            .All(y => y.FileString != x.Homework.FileString))
                        .ToList()
                        .ForEach(x => result.Add(x.Homework));

                    //foreach (var item in listOfUserLessonHomeworks)
                    //{
                    //    result.Where((x => !CurrentCollection.Any(y => x.bar == y.bar));)
                    //    result.Add(item.Homework);
                    //}
                }

                return result;
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
