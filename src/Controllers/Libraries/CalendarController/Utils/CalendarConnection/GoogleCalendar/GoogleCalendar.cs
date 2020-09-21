using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace LearnMe.Controllers.Libraries.CalendarController.Utils.CalendarConnection.GoogleCalendar
{
    public class GoogleCalendar : IExternalCalendarService<Event>
    {
        private readonly CalendarService calendarService;

        public GoogleCalendar(IGoogleAPIconnection googleAPIconnection,
                              string appName)
        {
            var token = googleAPIconnection.GetToken();
            calendarService = googleAPIconnection.CreateCalendarService(token, appName);
        }

        public void AddEvent(Event eventToAdd,
                             string calendarId = "primary")
        {
            var createdEvent = calendarService.Events.Insert(eventToAdd, calendarId).Execute();
            // TODO: Add to Calendar specific log
            Console.WriteLine($"Event created {createdEvent.HtmlLink}");
        }

        public void DeleteEvent(string eventId,
                                string calendarId = "primary")
        {
            var deletedEvent = calendarService.Events.Delete(eventId, calendarId).Execute();
            // TODO: Add to Calendar specific log
            Console.WriteLine($"Event deleted {deletedEvent.ToString()}");
        }

        public Event GetEventByDateTime(DateTime start,
                                        DateTime end,
                                        string calendarId = "primary")
        {
            EventsResource.ListRequest request = calendarService.Events.List(calendarId);
            request.TimeMin = start;
            request.TimeMax = end;
            request.ShowDeleted = false;
            request.MaxResults = 1;
            Event eventResult = request.Execute().Items.FirstOrDefault();

            // TODO: Add to Calendar specific log
            Console.WriteLine($"{eventResult.Summary}\n {eventResult.Location}\n {eventResult.Start.DateTime}");

            return eventResult.End.DateTime <= end ? eventResult : null;
        }

        public IEnumerable<Event> GetEvents(string calendarId = "primary")
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(string eventId,
                                Event eventToUpdate,
                                string calendarId = "primary")
        {
            throw new NotImplementedException();
        }
    }
}
