using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserBasic> _userManager;

        public CalendarEventsByDate(
            ICalendar calendar,
            IMapper mapper,
            ILogger<CalendarEventsController> logger,
            ICalendarEventsRepository calendarEventsRepository,
            UserManager<UserBasic> userManager)
        {
            _calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calendarEventsRepository = calendarEventsRepository ?? throw new ArgumentNullException(nameof(calendarEventsRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<ActionResult<IEnumerable<FullCalendarEventDto>>> GetAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //var result = await _calendar.GetFullEventsByDatesAsync(fromDate, toDate);

            ClaimsPrincipal currentUser = this.User;
            var currentUserRole = currentUser.FindFirst(ClaimTypes.Role).Value;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Name).Value;

            var result = await _calendar.GetFullEventsByUserRoleByDatesAsync(currentUserRole, currentUserEmail, fromDate, toDate);

            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

        //[Authorize(Roles = "Student")]
        //// GET: api/<controller>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<FullCalendarEventDto>>> GetForStudentUserAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    ClaimsPrincipal currentUser = this.User;
        //    var currentUserRole = currentUser.FindFirst(ClaimTypes.Role).Value;

        //    var result = await _calendar.GetFullEventsByUserRoleByDatesAsync(currentUserRole, fromDate, toDate);

        //    if (result != null)
        //    {
        //        return Ok(result);
        //    } else
        //    {
        //        return NotFound();
        //    }
        //}

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Mentor,Admin")]
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
        [Authorize(Roles = "Mentor,Admin")]
        public async Task<ActionResult<bool>> PutAsync([FromBody] FullCalendarEventDto eventData)
            => Ok(await _calendar.UpdateFullEventByCalendarIdAsync(eventData));

        // DELETE api/<controller>/5
        [HttpDelete("{calendarId}")]
        [Authorize(Roles = "Mentor,Admin")]
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
