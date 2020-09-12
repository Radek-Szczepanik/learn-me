using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Enum;
using System.Collections.Generic;
using LearnMe.Models.Domains.AdminDomain;

namespace LearnMe.Models.Domains.StudentDomain
{
    public class Lesson : BaseEntity
    {
        public int CalendarEventId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public CalendarEvent CalendarEvent { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string MessageText { get; set; }


        public IList<string> HomeworkFiles { get; set; }
        
        public IList<string> CorrectionFiles { get; set; }

        public IList<string> AdditionalFiles { get; set; }

        public LessonStatus LessonStatus { get; set; }
    }
}
