using System;

namespace LearnMe.Core.DTO.Calendar
{
    public class CalendarEventDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsDone { get; set; }

        public string CalendarId { get; set; }
    }
}
