using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Logging;
using LearnMe.Data;
using LearnMe.Models.Domains.Calendar;
using Microsoft.EntityFrameworkCore;

namespace LearnMe.Controllers.Libraries.CalendarController.BusinessLogic
{
    public class CalendarLogic
    {
        private readonly CalendarService _calendarService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CalendarLogic(CalendarService calendarService,
                             ApplicationDbContext context,
                             ILogger logger)
        {
            _calendarService = calendarService;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CalendarEvent>> GetEventsAsync()
        {
            //TODO Here add querying from db and returning db models
            //DTO model will be mapped in the controller

            throw new NotImplementedException();

            //// Define parameters of request.
            //EventsResource.ListRequest request = _calendarConnection.Events.List("primary");
            //request.TimeMin = DateTime.Now;
            //request.ShowDeleted = false;
            //request.SingleEvents = true;
            //request.MaxResults = 10;
            //request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //// List events.
            //Events events = request.Execute();
            //Console.WriteLine("Upcoming events:");
            //if (events.Items != null && events.Items.Count > 0)
            //{
            //    foreach (var eventItem in events.Items)
            //    {
            //        string when = eventItem.Start.DateTime.ToString();
            //        if (String.IsNullOrEmpty(when))
            //        {
            //            when = eventItem.Start.Date;
            //        }

            //        Console.WriteLine("{0} ({1})", eventItem.Summary, when);
            //    }
            //} else
            //{
            //    Console.WriteLine("No upcoming events found.");
            //}

            //return events.Items;
        }

        public async Task<CalendarEvent> FindCalendarEventByIdAsync(DbContext context, int? id)
        {
            if (id == null)
            {
                return null;
            }

            //var foundEvent = await context.CalendarEvents
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(e => e.Id == id);

            //return foundEvent;

            throw new NotImplementedException();
        }

        public async Task AddEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEventAsync(int? id)
        {
            var eventToDelete = await FindCalendarEventByIdAsync(_context, id);
            if (eventToDelete != null)
            {
                var eventId = eventToDelete.GoogleEventId;
                String calendarId = "primary";
                var deletedEvent = _calendarService.Events.Delete(calendarId, eventId).ExecuteAsync();

                _context.CalendarEvents.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
