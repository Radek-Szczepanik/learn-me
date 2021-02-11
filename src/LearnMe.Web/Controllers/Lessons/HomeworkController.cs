using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using LearnMe.Core.DTO.Lessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Invoice;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Primitives;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public HomeworkController(
            IHomeworkRepository homeworkRepository,
            ILessonsRepository lessonsRepository,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _homeworkRepository = homeworkRepository;
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
            _context = context;
        }

        //TODO
        /////GET: api/Homework
        //[HttpGet()]
        //public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworks(int lessonId)
        //{
        //    return await _context.Homeworks.ToListAsync();
        //}

        // GET: api/Homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> GetHomework(int id)
        {
            var homework = await _context.Homeworks.FindAsync(id);

            if (homework == null)
            {
                return NotFound();
            }

            return homework;
        }

        // PUT: api/Homework/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHomework(int id, Homework homework)
        //{
        //    if (id != homework.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(homework).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HomeworkExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Homework
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost(), DisableRequestSizeLimit]
        //public async Task<ActionResult> PostHomeworkFile()
        public async Task<ActionResult<HomeworkDto>> PostHomework()//[FromBody] HomeworkDto homework) //, string lessonCalendarId)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "Homeworks");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var calendarId = Request.Form.TryGetValue("lessonCalendarId", out var lessonCalendarId);
                var lesson = await _lessonsRepository.GetLessonByCalendarIdAsync(lessonCalendarId.ToString());
                if (lesson == null)
                {
                    return BadRequest();
                }

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

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

                    //return Ok(new { dbPath });

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

        //[HttpPost]
        //public async Task<ActionResult<Homework>> PostHomework(Homework homework)
        //{
        //    _context.Homeworks.Add(homework);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetHomework", new { id = homework.Id }, homework);
        //}

        // DELETE: api/Homework/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Homework>> DeleteHomework(int id)
        //{
        //    var homework = await _context.Homeworks.FindAsync(id);
        //    if (homework == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Homeworks.Remove(homework);
        //    await _context.SaveChangesAsync();

        //    return homework;
        //}

        //private bool HomeworkExists(int id)
        //{
        //    return _context.Homeworks.Any(e => e.Id == id);
        //}
    }
}
