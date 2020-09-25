using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Models.Domains.Lessons;

namespace LearnMe.Infrastructure.DbModels.Domains.Lessons
{
    public class Correction : BaseLessons
    {
      

        public string Feedback { get; set; }

        public UserLessonHomework UserLessonHomework { get; set; }
    }
}
