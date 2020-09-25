using System.Collections.Generic;
using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Infrastructure.DbModels.Domains.Lessons
{
    public class UserLesson : BaseEntity
    {
        public int UserId { get; set; }
        
        public UserBasic User { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public IList<Homework> Homeworks { get; set; }
    }
}
