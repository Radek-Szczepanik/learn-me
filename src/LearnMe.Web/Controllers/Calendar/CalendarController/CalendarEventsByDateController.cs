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
    public class CalendarEventsByDateController : Controller
    {
        private readonly ICalendar _calendar;
        private readonly ILogger<CalendarEventsController> _logger;

        public CalendarEventsByDateController(
            ICalendar calendar,
            ILogger<CalendarEventsController> logger)
        {
            _calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _calendar.GetEventsByDatesAsync(fromDate, toDate);
            
            if (result != null)
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
