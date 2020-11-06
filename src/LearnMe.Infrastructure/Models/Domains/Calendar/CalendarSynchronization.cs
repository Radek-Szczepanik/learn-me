using System;
using LearnMe.Infrastructure.Models.Base;

namespace LearnMe.Infrastructure.Models.Domains.Calendar
{
    public class CalendarSynchronization : BaseEntity
    {
        public DateTime? LastSynchronization { get; set; }
    }
}
