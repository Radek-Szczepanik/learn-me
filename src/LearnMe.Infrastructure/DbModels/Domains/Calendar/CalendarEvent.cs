using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Models.Domains.Lessons;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Infrastructure.DbModels.Domains.Calendar
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

        // Google Calendar specific properties
        public string GoogleEventId { get; set; }
    }
}