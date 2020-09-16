using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using LearnMe.Controllers.Libraries.CalendarController.Utils;
using LearnMe.Data;
using LearnMe.Models.Domains.Calendar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnMe.Controllers.Libraries.CalendarController
{
    [Route("api/[controller]")]
    public class CalendarEventsController : Controller
    {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string ApplicationName = "Learn Me WEB Applicaton";

        private readonly ILogger<CalendarEventsController> _logger;

        private readonly CalendarService _calendarService;

        private readonly ApplicationDbContext _context;

        public CalendarEventsController(
            ILogger<CalendarEventsController> logger,
            IGoogleAPIconnection googleAPIconnection,
            ApplicationDbContext context)
        {
            _logger = logger;
            var token = googleAPIconnection.GetToken();
            _calendarService = googleAPIconnection.CreateCalendarService(token, ApplicationName);
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            // Define parameters of request.
            EventsResource.ListRequest request = _calendarService.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }

                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            } else
            {
                Console.WriteLine("No upcoming events found.");
            }

            return events.Items;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var eventToDelete = await FindCalendarEventById(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            var eventId = eventToDelete.GoogleEventId;
            String calendarId = "primary";
            var deletedEvent = _calendarService.Events.Delete(calendarId, eventId).ExecuteAsync();
            _logger.LogInformation($"Event deleted from Google calendar: id {id}, Google calendar id {eventId}");

            _context.CalendarEvents.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Event deleted from DB: id {id}, Google calendar id {eventId}");

            return Ok();
        }

        private async Task<CalendarEvent> FindCalendarEventById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var foundEvent = await _context.CalendarEvents
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            return foundEvent;
        }
    }
}
