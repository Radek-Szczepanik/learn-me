using System;
using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LearnMe.Models.Domains.AdminDomain
{
    public class CalendarEvent : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public DateTime Start { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public DateTime End { get; set; }

        public IList<User> Attendees { get; set; }

        public bool IsDone { get; set; }
    }
}
