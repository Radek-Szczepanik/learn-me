using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Base;
using LearnMe.Infrastructure.Models.Domains.Users;

namespace LearnMe.Infrastructure.Models.Domains.Lessons
{
    public class UserLesson : BaseEntity
    {
        //public int UserId { get; set; }
        
        public UserBasic User { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public IList<Homework> Homeworks { get; set; }
    }
}
