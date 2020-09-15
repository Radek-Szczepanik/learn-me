using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Lessons
{
    public class Correction : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string FileName { get; set; }

        public string Feedback { get; set; }

        public UserLessonHomework UserLessonHomework { get; set; }
    }
}
