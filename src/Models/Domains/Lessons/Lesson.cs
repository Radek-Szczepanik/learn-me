using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Enum;
using System.Collections.Generic;
using LearnMe.Models.Domains.Calendar;
using LearnMe.Models.Domains.Invoice;

namespace LearnMe.Models.Domains.Lessons
{
    public class Lesson : BaseEntity
    {

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }
        public LessonStatus LessonStatus { get; set; }

        public InvoiceBasic RelatedInvoice { get; set; }

        public CalendarEvent CalendarEvent { get; set; }
        
        public int CalendarEventId { get; set; }

        public IList<UserLesson> UserLessons { get; set; }
    }
}
