using System.Collections.Generic;
using LearnMe.Models.Base;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Models.Domains.Lessons
{
    public class UserLesson : BaseEntity
    {
        public int UserId { get; set; }
        
        public User User { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public IList<Homework> Homeworks { get; set; }
    }
}
