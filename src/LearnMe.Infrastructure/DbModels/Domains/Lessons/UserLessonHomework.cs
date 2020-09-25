using LearnMe.Infrastructure.DbModels.Base;

namespace LearnMe.Infrastructure.DbModels.Domains.Lessons
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
