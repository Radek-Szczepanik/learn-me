using System;
using System.Collections.Generic;

namespace LearnMeAPI.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<User> Attendees { get; set; }
        public bool IsDone { get; set; }
    }
}
