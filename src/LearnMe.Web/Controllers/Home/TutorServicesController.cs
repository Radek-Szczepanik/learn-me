using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Home;

namespace LearnMe.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TutorServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TutorServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorService>>> GetTutorServices()
        {
            return await _context.TutorServices.ToListAsync();
        }

        // GET: api/TutorServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TutorService>> GetTutorService(int id)
        {
            var tutorService = await _context.TutorServices.FindAsync(id);

            if (tutorService == null)
            {
                return NotFound();
            }

            return tutorService;
        }

        // PUT: api/TutorServices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorService(int id, TutorService tutorService)
        {
            if (id != tutorService.Id)
            {
                return BadRequest();
            }

            _context.Entry(tutorService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorServiceExists(id))
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

        // POST: api/TutorServices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TutorService>> PostTutorService(TutorService tutorService)
        {
            _context.TutorServices.Add(tutorService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutorService", new { id = tutorService.Id }, tutorService);
        }

        // DELETE: api/TutorServices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TutorService>> DeleteTutorService(int id)
        {
            var tutorService = await _context.TutorServices.FindAsync(id);
            if (tutorService == null)
            {
                return NotFound();
            }

            _context.TutorServices.Remove(tutorService);
            await _context.SaveChangesAsync();

            return tutorService;
        }

        private bool TutorServiceExists(int id)
        {
            return _context.TutorServices.Any(e => e.Id == id);
        }
    }
}
