using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearnMe.Infrastructure.DbModels.Base;

namespace LearnMe.Infrastructure.DbModels.Domains.Lessons
{
    public class Homework : BaseLessons
    {
        
        [Required(ErrorMessage = "This field is required")]
        public string MessageText { get; set; }

        // 1 to many relation with UserLesson (1 UserLesson pair to many Homeworks)
        //public int UserLessonId { get; set; }

        public IList<UserLessonHomework> UserLessonHomeworkList { get; set; }
    }
}
