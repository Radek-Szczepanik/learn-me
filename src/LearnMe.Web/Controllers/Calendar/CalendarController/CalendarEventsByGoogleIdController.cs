﻿using System;
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
    public class CalendarEventsByGoogleIdController : Controller
    {
        private readonly ICalendar _calendar;
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarEventsController> _logger;
        private readonly ICalendarEventsRepository _calendarEventsRepository;

        public CalendarEventsByGoogleIdController(
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
