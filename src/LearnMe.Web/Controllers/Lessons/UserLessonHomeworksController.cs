using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Data;
using LearnMe.Models.Domains.Lessons;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLessonHomeworksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserLessonHomeworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLessonHomeworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLessonHomework>>> GetUserLessonHomeworks()
        {
            return await _context.UserLessonHomeworks.ToListAsync();
        }

        // GET: api/UserLessonHomeworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLessonHomework>> GetUserLessonHomework(int id)
        {
            var userLessonHomework = await _context.UserLessonHomeworks.FindAsync(id);

            if (userLessonHomework == null)
            {
                return NotFound();
            }

            return userLessonHomework;
        }

        // PUT: api/UserLessonHomeworks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLessonHomework(int id, UserLessonHomework userLessonHomework)
        {
            if (id != userLessonHomework.Id)
            {
                return BadRequest();
            }

            _context.Entry(userLessonHomework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLessonHomeworkExists(id))
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

        // POST: api/UserLessonHomeworks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserLessonHomework>> PostUserLessonHomework(UserLessonHomework userLessonHomework)
        {
            _context.UserLessonHomeworks.Add(userLessonHomework);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLessonHomework", new { id = userLessonHomework.Id }, userLessonHomework);
        }

        // DELETE: api/UserLessonHomeworks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserLessonHomework>> DeleteUserLessonHomework(int id)
        {
            var userLessonHomework = await _context.UserLessonHomeworks.FindAsync(id);
            if (userLessonHomework == null)
            {
                return NotFound();
            }

            _context.UserLessonHomeworks.Remove(userLessonHomework);
            await _context.SaveChangesAsync();

            return userLessonHomework;
        }

        private bool UserLessonHomeworkExists(int id)
        {
            return _context.UserLessonHomeworks.Any(e => e.Id == id);
        }
    }
}
