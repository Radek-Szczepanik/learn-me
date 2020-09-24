using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Infrastructure.Models.Domains.Lessons
{
    public class Correction : BaseLessons
    {
      

        public string Feedback { get; set; }

        public UserLessonHomework UserLessonHomework { get; set; }
    }
}
