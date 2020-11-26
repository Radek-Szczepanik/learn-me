using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Lessons;
using LearnMe.Core.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonsController> _logger;

        public LessonsController(
            ILessonsRepository lessonsRepository,
            IMapper mapper,
            ILogger<LessonsController> logger)
        {
            _lessonsRepository = lessonsRepository ?? throw new ArgumentNullException(nameof(lessonsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Lessons/5
        [HttpGet("{calendarEventId}", Name = "LessonByCalendarId")]
        public async Task<ActionResult<LessonDto>> GetLessonByCalendarEventIdAsync(string calendarEventId)
        {
            var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(calendarEventId);

            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{calendarEventId}")]
        public async Task<ActionResult<bool>> PutLessonByCalendarEventIdAsync(string calendarEventId, [FromBody] LessonDto lesson)
        {
            var lessonData = _mapper.Map<Lesson>(lesson);
            var isUpdated = await _lessonsRepository.UpdateLessonByCalendarIdAsync(calendarEventId, lessonData);
            
            if (isUpdated)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Lessons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{calendarEventId}")]
        public async Task<ActionResult<LessonDto>> PostLessonWithCalendarEventIdAsync(string calendarEventId, LessonDto lesson)
        {
            var lessonData = _mapper.Map<Lesson>(lesson);
            var newLessonDbObject = await _lessonsRepository.CreateLessonAsync(calendarEventId, lessonData);

            if (newLessonDbObject != null)
            {
                return CreatedAtRoute(
                    "LessonByCalendarId", new { calendarEventId = calendarEventId }, lesson);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{calendarEventId}")]
        public async Task<ActionResult<bool>> DeleteLessonByCalendarEventId(string calendarEventId)
        {
            var result = await _lessonsRepository.DeleteLessonByCalendarIdAsync(calendarEventId);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Lessons/5/Attendees
        [HttpGet("{calendarEventId}/attendees", Name = "LessonAttendeesByCalendarId")]
        public async Task<ActionResult<IList<UserBasicDto>>> GetLessonAttendeesByCalendarEventIdAsync(string calendarEventId)
        {
            var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(calendarEventId);
            var attendees = await _lessonsRepository.GetLessonAttendeesAsync(lesson);

            IList<UserBasicDto> attendeesDtos = new List<UserBasicDto>();

            if (attendees == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var person in attendees)
                {
                    attendeesDtos.Add(_mapper.Map<UserBasicDto>(person));
                }
            }

            return Ok(attendeesDtos);
        }

        //// PUT: api/Lessons/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{calendarEventId}")]
        //public async Task<ActionResult<bool>> PutLessonByCalendarEventIdAsync(string calendarEventId, [FromBody] LessonDto lesson)
        //{
        //    var lessonData = _mapper.Map<Lesson>(lesson);
        //    var isUpdated = await _lessonsRepository.UpdateLessonByCalendarIdAsync(calendarEventId, lessonData);

        //    if (isUpdated)
        //    {
        //        return Ok();
        //    } else
        //    {
        //        return BadRequest();
        //    }
        //}

        // POST: api/Lessons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{calendarEventId}/attendees")]
        public async Task<ActionResult<UserBasicDto>> PostAttendeeToLessonByCalendarEventIdAsync(string calendarEventId, string attendeeEmail)
        {
            var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(calendarEventId);
            var newLessonDbObject = await _lessonsRepository.CreateLessonAttendeeAsync(lesson, attendeeEmail);

            var result = _mapper.Map<UserBasicDto>(newLessonDbObject);

            if (newLessonDbObject != null)
            {
                return CreatedAtRoute(
                    "LessonAttendeesByCalendarId", new { calendarEventId = calendarEventId }, result);
            } else
            {
                return BadRequest();
            }
        }
    }
}
