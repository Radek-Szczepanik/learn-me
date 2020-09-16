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
    public class CorrectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CorrectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Corrections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Correction>>> GetCorrections()
        {
            return await _context.Corrections.ToListAsync();
        }

        // GET: api/Corrections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Correction>> GetCorrection(int id)
        {
            var correction = await _context.Corrections.FindAsync(id);

            if (correction == null)
            {
                return NotFound();
            }

            return correction;
        }

        // PUT: api/Corrections/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorrection(int id, Correction correction)
        {
            if (id != correction.Id)
            {
                return BadRequest();
            }

            _context.Entry(correction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorrectionExists(id))
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

        // POST: api/Corrections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Correction>> PostCorrection(Correction correction)
        {
            _context.Corrections.Add(correction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCorrection", new { id = correction.Id }, correction);
        }

        // DELETE: api/Corrections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Correction>> DeleteCorrection(int id)
        {
            var correction = await _context.Corrections.FindAsync(id);
            if (correction == null)
            {
                return NotFound();
            }

            _context.Corrections.Remove(correction);
            await _context.SaveChangesAsync();

            return correction;
        }

        private bool CorrectionExists(int id)
        {
            return _context.Corrections.Any(e => e.Id == id);
        }
    }
}
