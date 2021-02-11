using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using LearnMe.Infrastructure.Repository.Interfaces;

namespace LearnMe.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrectionsController : ControllerBase
    {
        private readonly ICorrectionRepository _correctionRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CorrectionsController(
            ICorrectionRepository correctionRepository,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _correctionRepository = correctionRepository;
            _mapper = mapper;
            _context = context;
        }

        //// GET: api/Corrections
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Correction>>> GetCorrections()
        //{
        //    return await _context.Corrections.ToListAsync();
        //}

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

        //// DELETE: api/Corrections/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Correction>> DeleteCorrection(int id)
        //{
        //    var correction = await _context.Corrections.FindAsync(id);
        //    if (correction == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Corrections.Remove(correction);
        //    await _context.SaveChangesAsync();

        //    return correction;
        //}

        private bool CorrectionExists(int id)
        {
            return _context.Corrections.Any(e => e.Id == id);
        }
    }
}
