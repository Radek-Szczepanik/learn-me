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

        //// GET: api/<controller>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var result = await _calendar.GetEventsByDatesAsync(fromDate, toDate);

        //    if (result != null)
        //    {
        //        return Ok(result);
        //    } else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
