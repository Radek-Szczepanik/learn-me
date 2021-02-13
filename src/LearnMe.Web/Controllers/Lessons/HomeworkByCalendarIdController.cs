using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Lessons;
using Microsoft.AspNetCore.Mvc;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkByCalendarIdController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;

        public HomeworkByCalendarIdController(
            IHomeworkRepository homeworkRepository,
            ILessonsRepository lessonsRepository,
            IMapper mapper)
        {
            _homeworkRepository = homeworkRepository;
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }

        ///GET: api/Homework
        [HttpGet("{lessonCalendarId}")]
        public async Task<ActionResult<IEnumerable<HomeworkDto>>> GetHomeworks(string lessonCalendarId)
        {
            var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(lessonCalendarId);
            if (lesson == null)
            {
                return BadRequest();
            }

            var homeworksForGivenLesson = await _homeworkRepository.GetAllHomeworksByLessonIdAsync(lesson.Id);

            var result = new List<HomeworkDto>();

            foreach (var item in homeworksForGivenLesson)
            {
                result.Add(_mapper.Map<HomeworkDto>(item));
            }

            return Ok(result);
        }
    }
}
