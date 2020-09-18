using System;
using System.Collections.Generic;


namespace LearnMe.Controllers.Libraries.CalendarController.Utils.CalendarConnection
{
    public interface IExternalCalendarService<T> where T : class, new()
    {
        IEnumerable<T> GetEvents(string calendarId);

        T GetEventByDateTime(DateTime start, DateTime end, string calendarId);
        
        void AddEvent(T eventToAdd, string calendarId);
        
        void UpdateEvent(string eventId, T eventToUpdate, string calendarId);
        
        void DeleteEvent(string eventId, string calendarId);
    }

}
