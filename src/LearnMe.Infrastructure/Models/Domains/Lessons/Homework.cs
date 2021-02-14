using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Infrastructure.Models.Domains.Lessons
{
    public class Homework : BaseLessons
    {

        [Required(ErrorMessage = "This field is required")]
        public string MessageText { get; set; }

        // 1 to many relation with UserLesson (1 UserLesson pair to many Homeworks)
        //public int UserLessonId { get; set; }

        public int? HomeworkTypeId { get; set; }

        public HomeworkType HomeworkType { get; set; }

        public IList<UserLessonHomework> UserLessonHomeworkList { get; set; }
    }
}
