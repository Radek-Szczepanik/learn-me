using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnMe.Web.Controllers.Calendar.CalendarController
{
    [Authorize(Roles = "Mentor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsByDate : Controller
    {
        private readonly ICalendar _calendar;
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarEventsController> _logger;
        private readonly ICalendarEventsRepository _calendarEventsRepository;

        public CalendarEventsByDate(
            ICalendar calendar,
            IMapper mapper,
            ILogger<CalendarEventsController> logger,
            ICalendarEventsRepository calendarEventsRepository)
        {
            _calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calendarEventsRepository = calendarEventsRepository ?? throw new ArgumentNullException(nameof(calendarEventsRepository));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullCalendarEventDto>>> GetAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _calendar.GetFullEventsByDatesAsync(fromDate, toDate);

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
        public async Task<ActionResult<bool>> PostAsync([FromBody] FullCalendarEventDto eventData)
        {
            var newEvent = await _calendar.CreateFullEventAsync(eventData);

            if (newEvent != null)
            {
                //Added calendarId to DTO
                var newEventDbObject = await _calendarEventsRepository.GetByCalendarIdAsync(newEvent.CalendarId);

                return CreatedAtRoute("EventById", new { id = newEventDbObject.Id }, newEvent);
            } else
            {
                return BadRequest();
            }
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<ActionResult<bool>> PutAsync([FromBody] FullCalendarEventDto eventData)
            => Ok(await _calendar.UpdateFullEventByCalendarIdAsync(eventData));

        // DELETE api/<controller>/5
        [HttpDelete("{calendarId}")]
        public async Task<ActionResult<bool>> DeleteAsync(string calendarId)
        {
            var result = await _calendar.DeleteFullEventByCalendarIdAsync(calendarId);

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
