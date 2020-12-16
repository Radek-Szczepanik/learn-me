using System;
using System.Collections.Generic;
using LearnMe.Shared.Enum;

namespace LearnMe.Core.DTO.Lessons
{
    public class FullLessonDto
    {
        // Lesson related
        public string Title { get; set; }

        public LessonStatus LessonStatus { get; set; }

        public int? RelatedInvoiceId { get; set; }

        public int CalendarEventId { get; set; }

        // Event related
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDone { get; set; }

        public bool IsFreeSlot { get; set; }

        public string CalendarId { get; set; }

        // Attendee related
        public IList<string> Attendees { get; set; }
    }
}