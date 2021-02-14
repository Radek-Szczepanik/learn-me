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
    public class HomeworkDoneController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserBasic> _userManager;

        public HomeworkDoneController(
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
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<HomeworkDto>>> GetHomeworksDoneForLessonAsync(string lessonCalendarId)
        {
            var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(lessonCalendarId);
            if (lesson == null)
            {
                return BadRequest();
            }

            var userHomeworksUploadedForGivenLesson = await _homeworkRepository.GetAllHomeworksTypeDoneByLessonIdAsync(lesson.Id);

            var result = new List<HomeworkDto>();

            foreach (var item in userHomeworksUploadedForGivenLesson)
            {
                result.Add(_mapper.Map<HomeworkDto>(item));
            }

            return Ok(result);
        }
    }
}
