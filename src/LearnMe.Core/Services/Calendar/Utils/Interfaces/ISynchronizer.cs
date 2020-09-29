﻿using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Core.Services.Calendar.Utils.Interfaces
{
    public interface ISynchronizer
    {
        Task<int> SynchronizeDatabaseWithCalendarAsync(
            ICalendarService<Event> calendarService, 
            ICrudRepository<CalendarEvent> repository, 
            string calendarId = Constants.CalendarId);
    }
}
