using System;

namespace LearnMe.Controllers.Libraries.CalendarController.DTO
{
    public class CalendarEventDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsDone { get; set; }
    }
}
