using System.Collections.Generic;
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

        public CalendarEventsController(ICalendar calendar,
                                        ILogger<CalendarEventsController> logger)
        {
            _calendar = calendar;
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetAsync()
            => Ok(await _calendar.GetAllEventsAsync());

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEventDto>> GetByIdAsync(int id)
            => Ok(await _calendar.GetEventByIdAsync(id));

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
            => Ok(await _calendar.DeleteEventAsync(id));
    }
}
