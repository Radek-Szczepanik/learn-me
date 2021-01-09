using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Shared.Enum;
using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Invoice;

namespace LearnMe.Infrastructure.Models.Domains.Lessons
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
