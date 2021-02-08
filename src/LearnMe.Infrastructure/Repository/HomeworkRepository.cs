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

        public Task<bool> DeleteHomeworkByLessonIdAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Homework>> GetAllHomeworksByLessonIdAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        public Task<Homework> GetHomeworkByFileNameAndLessonIdAsync(int lessonId)
        {
            throw new NotImplementedException();
        }

        public async Task<Homework> InsertHomeworkByLessonIdAsync(Homework homework, int lessonId)
        {
            var lesson = await _context.Lessons
                .Where(x => x.Id == lessonId)
                .Include(x => x.UserLessons)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            //List<int> userLessonsToAddHomework = new List<int>();
            homework.UserLessonHomeworkList ??= new List<UserLessonHomework>();

            foreach (var student in lesson.UserLessons)
            {
                student.Homeworks.Add(homework);

                _context.Lessons.Update(lesson);
                await _context.SaveChangesAsync();

                //userLessonsToAddHomework.Add(student.Id);

                //student.Lesson.RelatedInvoice ??= new InvoiceBasic()
                //{
                //    Student = student.User
                //};

                //To nic nie poprawia
                //student.Lesson.RelatedInvoice.Student = student.User;
                //student.User.InvoicesList ??= new List<InvoiceBasic>()
                //{
                //    new InvoiceBasic()
                //    {
                //        Student = student.User,
                //    }
                //};
                //foreach (var item in student.Lesson.CalendarEvent.Attendees)
                //{
                //    item.InvoicesList ??= new List<InvoiceBasic>();
                //    item.InvoicesList.Add(new InvoiceBasic()
                //    {
                //        Student = item,
                //    });
                //}


                //homework.UserLessonHomeworkList.Add(new UserLessonHomework()
                //{
                //    UserLesson = student,
                //    Homework = homework,
                //    Correction = new Correction()
                //});
            }

            return homework;
            //return await InsertAsync(homework);

            //foreach (var student in lesson.UserLessons)
            //{
            //    student.Homeworks ??= new List<Homework>();

            //    student.Homeworks.Add(homework);
            //}

            //_context.Lessons.Update(lesson);

            //return homework;
        }
    }
}
