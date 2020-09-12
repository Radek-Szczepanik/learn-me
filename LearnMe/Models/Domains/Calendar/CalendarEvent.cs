using System;
using LearnMe.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LearnMe.Models.Domains.Lessons;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Models.Domains.Calendar
{
    public class CalendarEvent : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }

        public bool IsDone { get; set; }

        [NotMapped]
        public IList<User> Attendees { get; set; }

        public Lesson Lesson { get; set; }
    }
}
