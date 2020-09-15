using System.Collections.Generic;
using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Lessons
{
    public class Homework : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string FileName { get; set; }

        // 1 to many relation with UserLesson (1 UserLesson pair to many Homeworks)
        //public int UserLessonId { get; set; }

        public IList<UserLessonHomework> UserLessonHomeworkList { get; set; }
    }
}
