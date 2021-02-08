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

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public HomeworkController(
            IHomeworkRepository homeworkRepository,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _homeworkRepository = homeworkRepository;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Homework
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworks()
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
        [HttpPost("lessonId"), DisableRequestSizeLimit]
        //public async Task<ActionResult> PostHomeworkFile()
        public async Task<ActionResult<Homework>> PostHomework([FromBody] HomeworkDto homework, int lessonId)
        {
            //try
            //{
            //    var file = Request.Form.Files[0];
            //    var folderName = Path.Combine("wwwroot", "Homeworks");
            //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //    if (file.Length > 0)
            //    {
            //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //        var fullPath = Path.Combine(pathToSave, fileName);
            //        var dbPath = Path.Combine(folderName, fileName);
            //        using (var stream = new FileStream(fullPath, FileMode.Create))
            //        {
            //            file.CopyTo(stream);
            //        }
            //        return Ok(new { dbPath });
            //    } else
            //    {
            //        return BadRequest();
            //    }
            //} catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex}");
            //}

            //var result = await _homeworkRepository.InsertHomeworkByLessonIdAsync(homework, lessonId);

            //return Ok(result);


            //homework.UserLessonHomeworkList = new List<UserLessonHomework>()
            //{
            //    new UserLessonHomework()
            //    {
            //        UserLesson = new UserLesson()
            //        {
            //            Lesson = new Lesson()
            //            {
            //                RelatedInvoice = new InvoiceBasic()
            //                {
            //                    Student = new UserBasic()
            //                },
            //                CalendarEvent = new CalendarEvent()
            //                {
            //                    Attendees = new List<UserBasic>()
            //                    {
            //                        new UserBasic()
            //                        {
            //                            InvoicesList = new List<InvoiceBasic>()
            //                            {
            //                                new InvoiceBasic()
            //                                {
            //                                    Student = new UserBasic()
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            },
            //            User = new UserBasic()
            //            {
            //                InvoicesList = new List<InvoiceBasic>()
            //                {
            //                    new InvoiceBasic()
            //                    {
            //                        Student = new UserBasic()
            //                    }
            //                }
            //            }
            //        }
            //    }
            //};

            var homeworkData = _mapper.Map<Homework>(homework);

            // WORKING
            //_context.Homeworks.Add(homeworkData);
            //await _context.SaveChangesAsync();

            var result = await _homeworkRepository.InsertHomeworkByLessonIdAsync(homeworkData, lessonId);

            return Ok(result);

            //return CreatedAtAction("GetHomework", new { id = homeworkData.Id }, homeworkData);

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
