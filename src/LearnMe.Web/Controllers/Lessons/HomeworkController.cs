using System;
using System.IO;
using System.Net.Http.Headers;
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
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;

        public HomeworkController(
            IHomeworkRepository homeworkRepository,
            ILessonsRepository lessonsRepository,
            IMapper mapper)
        {
            _homeworkRepository = homeworkRepository;
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }

        // GET: api/Homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> GetHomework(int id)
        {
            var homework = await _homeworkRepository.GetByIdAsync(id);

            if (homework == null)
            {
                return NotFound();
            }

            return homework;
        }

        // POST: api/Homework
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost(), DisableRequestSizeLimit]
        public async Task<ActionResult<HomeworkDto>> PostHomework()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "Homeworks");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                Request.Form.TryGetValue("lessonCalendarId", out var lessonCalendarId);
                var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(lessonCalendarId.ToString());
                if (lesson == null)
                {
                    return BadRequest();
                }

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    var fileNameAndExtension = fileName.Split('.');
                    var datetime = DateTime.UtcNow;
                    var timestamp =
                        $"{datetime.Year}{datetime.Month}{datetime.Day}_{datetime.Hour}{datetime.Minute}{datetime.Second}";
                    var fileNameWithTimestamp = $"{fileNameAndExtension[0]}_{timestamp}.{fileNameAndExtension[1]}";
                    var newPath = Path.Combine(pathToSave, fileNameWithTimestamp);
                    await using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var homework = new HomeworkDto()
                    {
                        FileString = fileNameWithTimestamp,
                        MessageText = ""
                    };

                    var homeworkData = _mapper.Map<Homework>(homework);
                    var result = await _homeworkRepository.InsertHomeworkByLessonIdAsync(homeworkData, lesson.Id);

                    return Created("", _mapper.Map<HomeworkDto>(result));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
