using LearnMe.Models.Base;
using System.Collections.Generic;

namespace LearnMe.Models.Domains.Lessons
{
    public class UserLessonHomework : BaseEntity
    {
        public int UserLessonId { get; set; }

        public UserLesson UserLesson { get; set; }

        public int HomeworkId { get; set; }

        public Homework Homework { get; set; }

        public int CorrectionId { get; set; }

        public Correction Correction { get; set; }
    }
}
