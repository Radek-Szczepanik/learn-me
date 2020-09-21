using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Data;
using LearnMe.Models.Domains.Invoice;

namespace LearnMe.Controllers.Invoice
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceBasicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoiceBasicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceBasics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceBasic>>> GetInvoiceBasics()
        {
            return await _context.InvoiceBasics.ToListAsync();
        }

        // GET: api/InvoiceBasics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceBasic>> GetInvoiceBasic(int id)
        {
            var invoiceBasic = await _context.InvoiceBasics.FindAsync(id);

            if (invoiceBasic == null)
            {
                return NotFound();
            }

            return invoiceBasic;
        }

        // PUT: api/InvoiceBasics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceBasic(int id, InvoiceBasic invoiceBasic)
        {
            if (id != invoiceBasic.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceBasic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceBasicExists(id))
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

        // POST: api/InvoiceBasics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InvoiceBasic>> PostInvoiceBasic(InvoiceBasic invoiceBasic)
        {
            _context.InvoiceBasics.Add(invoiceBasic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceBasic", new { id = invoiceBasic.Id }, invoiceBasic);
        }

        // DELETE: api/InvoiceBasics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceBasic>> DeleteInvoiceBasic(int id)
        {
            var invoiceBasic = await _context.InvoiceBasics.FindAsync(id);
            if (invoiceBasic == null)
            {
                return NotFound();
            }

            _context.InvoiceBasics.Remove(invoiceBasic);
            await _context.SaveChangesAsync();

            return invoiceBasic;
        }

        private bool InvoiceBasicExists(int id)
        {
            return _context.InvoiceBasics.Any(e => e.Id == id);
        }
    }
}
