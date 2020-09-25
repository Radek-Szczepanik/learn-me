using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Infrastructure.DbModels.Domains.Calendar;
using LearnMe.Infrastructure.DbModels.Domains.Invoice;
using LearnMe.Infrastructure.Enum;

namespace LearnMe.Infrastructure.DbModels.Domains.Lessons
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
