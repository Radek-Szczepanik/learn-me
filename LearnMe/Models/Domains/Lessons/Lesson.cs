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
        public int CalendarEventId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string MessageText { get; set; }

        public LessonStatus LessonStatus { get; set; }

        public InvoiceBasic RelatedInvoice { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public CalendarEvent CalendarEvent { get; set; }

        public IList<UserLesson> UserLessons { get; set; }
    }
}
