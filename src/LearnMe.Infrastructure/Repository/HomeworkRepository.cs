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

        public async Task<IList<Homework>> GetAllHomeworksByLessonIdAsync(int lessonId, string userId = null)
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
                    List<UserLessonHomework> listOfUserLessonHomeworks;
                    if (userId == null)
                    {
                        listOfUserLessonHomeworks = await _context.UserLessonHomeworks
                            .Where(x => x.UserLesson == userLesson
                                        && x.Homework.HomeworkType.Type == "Todo")
                            .Include(x => x.Homework)
                            .AsNoTracking()
                            .ToListAsync();
                    }
                    else
                    {
                        listOfUserLessonHomeworks = await _context.UserLessonHomeworks
                            .Where(x => x.UserLesson.UserId == userId
                                        && x.UserLesson.LessonId == lessonId
                                        && x.Homework.HomeworkType.Type == "Done")
                            .Include(x => x.Homework)
                            .AsNoTracking()
                            .ToListAsync();
                    }

                    listOfUserLessonHomeworks
                        .Where(x => result
                            .All(y => y.FileString != x.Homework.FileString))
                        .ToList()
                        .ForEach(x => result.Add(x.Homework));
                }

                return result;
            }

            return new List<Homework>();
        }

        public async Task<Homework> GetHomeworkByFileNameAndLessonIdAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        public async Task<Homework> InsertHomeworkByLessonIdAsync(Homework homework, int lessonId, string userId = "")
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
            
            if(userId == "")
            {
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
            }
            else
            {
                var student = lesson.UserLessons
                    .SingleOrDefault(x => x.UserId == userId);

                if (student != null)
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
