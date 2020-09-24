using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Lessons;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLessonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserLessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLesson>>> GetUserLessons()
        {
            return await _context.UserLessons.ToListAsync();
        }

        // GET: api/UserLessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLesson>> GetUserLesson(int id)
        {
            var userLesson = await _context.UserLessons.FindAsync(id);

            if (userLesson == null)
            {
                return NotFound();
            }

            return userLesson;
        }

        // PUT: api/UserLessons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLesson(int id, UserLesson userLesson)
        {
            if (id != userLesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(userLesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLessonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserLessons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserLesson>> PostUserLesson(UserLesson userLesson)
        {
            _context.UserLessons.Add(userLesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLesson", new { id = userLesson.Id }, userLesson);
        }

        // DELETE: api/UserLessons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserLesson>> DeleteUserLesson(int id)
        {
            var userLesson = await _context.UserLessons.FindAsync(id);
            if (userLesson == null)
            {
                return NotFound();
            }

            _context.UserLessons.Remove(userLesson);
            await _context.SaveChangesAsync();

            return userLesson;
        }

        private bool UserLessonExists(int id)
        {
            return _context.UserLessons.Any(e => e.Id == id);
        }
    }
}
