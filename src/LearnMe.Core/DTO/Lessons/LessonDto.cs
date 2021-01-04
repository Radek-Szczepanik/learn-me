using LearnMe.Shared.Enum;

namespace LearnMe.Core.DTO.Lessons
{
    public class LessonDto
    {
        public string Title { get; set; }

        public LessonStatus LessonStatus { get; set; }

        public int? RelatedInvoiceId { get; set; }

        public int CalendarEventId { get; set; }
    }
}
