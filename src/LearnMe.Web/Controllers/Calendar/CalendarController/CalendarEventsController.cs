using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnMe.Web.Controllers.Calendar.CalendarController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : Controller
    {
        private readonly ICalendar _calendar;
        private readonly ILogger<CalendarEventsController> _logger;

        public CalendarEventsController(
            ICalendar calendar,
            ILogger<CalendarEventsController> logger)
        {
            _calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Ok(await _calendar.GetAllEventsAsync());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEventDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // TODO: Consider another option:
            // The below code returns 204 No Content when id not found:
            //return Ok(await _calendar.GetEventByIdAsync(id)); 

            var result = await _calendar.GetEventByIdAsync(id);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<bool>> PostAsync([FromBody] CalendarEventDto eventData)
            => Ok(await _calendar.CreateEventAsync(eventData));

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutAsync(int id, [FromBody] CalendarEventDto eventData)
            => Ok(await _calendar.UpdateEventAsync(id, eventData));

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _calendar.DeleteEventAsync(id);

            if (result)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
    }
}
