using System;
using LearnMe.Infrastructure.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;

namespace LearnMe.Infrastructure.Models.Domains.Calendar
{
    public class CalendarEvent : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }

        public bool IsDone { get; set; }

        [NotMapped]
        public IList<UserBasic> Attendees { get; set; }

        public Lesson Lesson { get; set; }
    }
}
