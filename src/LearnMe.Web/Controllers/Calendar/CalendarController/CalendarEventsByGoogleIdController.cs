using System;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnMe.Web.Controllers.Calendar.CalendarController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsByGoogleIdController : Controller
    {
        private readonly ICalendar _calendar;
        private readonly ILogger<CalendarEventsController> _logger;

        public CalendarEventsByGoogleIdController(
            ICalendar calendar,
            ILogger<CalendarEventsController> logger)
        {
            _calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET api/<controller>/5
        [HttpGet("{calendarId}")]
        public async Task<ActionResult<CalendarEventDto>> GetByExternalCalendarIdAsync(string calendarId)
        {
            var result = await _calendar.GetEventByExternalCalendarIdAsync(calendarId);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<ActionResult<bool>> PutAsync([FromBody] CalendarEventDto eventData)
            => Ok(await _calendar.UpdateEventByCalendarIdAsync(eventData));

        // DELETE api/<controller>/5
        [HttpDelete("{calendarId}")]
        public async Task<ActionResult<bool>> DeleteAsync(string calendarId)
        {
            var result = await _calendar.DeleteEventByCalendarIdAsync(calendarId);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
