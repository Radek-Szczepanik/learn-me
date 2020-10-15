using System;

namespace LearnMe.Core.DTO.Calendar
{
    public class CalendarEventDto
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDone { get; set; }

        public bool IsFreeSlot { get; set; }

        public string CalendarId { get; set; }
    }
}
