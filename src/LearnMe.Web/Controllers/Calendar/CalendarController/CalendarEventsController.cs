using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnMe.Web.Controllers.Calendar.CalendarController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : Controller
    {
        private readonly ICalendar _calendar;
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarEventsController> _logger;
        private readonly ICalendarEventsRepository _calendarEventsRepository;

        public CalendarEventsController(
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
        public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetAsync(int eventsPerPage, int pageNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _calendar.GetAllEventsAsync(eventsPerPage, pageNumber);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "EventById")]
        public async Task<ActionResult<CalendarEventDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _calendar.GetEventByIdAsync(id);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

        //// POST api/<controller>
        //[HttpPost]
        //public async Task<ActionResult<bool>> PostAsync([FromBody] CalendarEventDto eventData)
        //{
        //    var newEvent = await _calendar.CreateEventAsync(eventData);

        //    //if (newEvent != null)
        //    //{
        //    //Added calendarId to DTO
        //    var newEventDbObject = await _calendarEventsRepository.GetByCalendarIdAsync(newEvent.CalendarId);

        //    return CreatedAtRoute("EventById", new { id = newEventDbObject.Id }, newEvent);
        //    //}
        //    //else
        //    //{
        //    //    return NotFound();
        //    //}
        //}

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
            }
            else
            {
                return NotFound();
            }
        }
    }
}
