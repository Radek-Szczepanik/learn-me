using System;
using LearnMe.Core.Services.Calendar.Utils.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Implementations
{
    public class Date : IDate
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
