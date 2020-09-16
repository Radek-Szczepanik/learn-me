using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Lessons
{
    public class Correction : BaseLessons
    {
      

        public string Feedback { get; set; }

        public UserLessonHomework UserLessonHomework { get; set; }
    }
}
