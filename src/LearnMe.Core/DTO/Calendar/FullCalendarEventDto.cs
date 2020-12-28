using System;
using System.Collections.Generic;
using LearnMe.Core.DTO.Lessons;
using LearnMe.Core.DTO.User;

namespace LearnMe.Core.DTO.Calendar
{
    public class FullCalendarEventDto
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDone { get; set; }

        public bool IsFreeSlot { get; set; }

        public string CalendarId { get; set; }

        public LessonDto Lesson { get; set; }

        public IList<UserBasicDto> Attendees { get; set; }
    }
}
