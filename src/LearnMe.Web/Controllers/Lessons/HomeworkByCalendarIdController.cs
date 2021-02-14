using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Lessons;
using Microsoft.AspNetCore.Mvc;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkByCalendarIdController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserBasic> _userManager;

        public HomeworkByCalendarIdController(
            IHomeworkRepository homeworkRepository,
            ILessonsRepository lessonsRepository,
            IMapper mapper,
            UserManager<UserBasic> userManager)
        {
            _homeworkRepository = homeworkRepository;
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        //GET: api/Homework
        [HttpGet("{lessonCalendarId}")]
        public async Task<ActionResult<IEnumerable<HomeworkDto>>> GetHomeworksAsync(string lessonCalendarId)
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

        //GET: api/Homework
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<HomeworkDto>>> GetHomeworksForUserAsync(string lessonCalendarId, string userEmail)
        {
            var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(lessonCalendarId);
            if (lesson == null)
            {
                return BadRequest();
            }
            
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userHomeworksUploadedForGivenLesson = await _homeworkRepository.GetAllHomeworksByLessonIdAsync(lesson.Id, user.Id);

            var result = new List<HomeworkDto>();

            foreach (var item in userHomeworksUploadedForGivenLesson)
            {
                result.Add(_mapper.Map<HomeworkDto>(item));
            }

            return Ok(result);
        }
    }
}
