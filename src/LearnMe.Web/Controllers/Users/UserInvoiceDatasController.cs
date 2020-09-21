using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Data;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInvoiceDatasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserInvoiceDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserInvoiceDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInvoiceData>>> GetUserInvoiceDatas()
        {
            return await _context.UserInvoiceDatas.ToListAsync();
        }

        // GET: api/UserInvoiceDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInvoiceData>> GetUserInvoiceData(int id)
        {
            var userInvoiceData = await _context.UserInvoiceDatas.FindAsync(id);

            if (userInvoiceData == null)
            {
                return NotFound();
            }

            return userInvoiceData;
        }

        // PUT: api/UserInvoiceDatas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInvoiceData(int id, UserInvoiceData userInvoiceData)
        {
            if (id != userInvoiceData.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInvoiceData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInvoiceDataExists(id))
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

        // POST: api/UserInvoiceDatas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserInvoiceData>> PostUserInvoiceData(UserInvoiceData userInvoiceData)
        {
            _context.UserInvoiceDatas.Add(userInvoiceData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInvoiceData", new { id = userInvoiceData.Id }, userInvoiceData);
        }

        // DELETE: api/UserInvoiceDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInvoiceData>> DeleteUserInvoiceData(int id)
        {
            var userInvoiceData = await _context.UserInvoiceDatas.FindAsync(id);
            if (userInvoiceData == null)
            {
                return NotFound();
            }

            _context.UserInvoiceDatas.Remove(userInvoiceData);
            await _context.SaveChangesAsync();

            return userInvoiceData;
        }

        private bool UserInvoiceDataExists(int id)
        {
            return _context.UserInvoiceDatas.Any(e => e.Id == id);
        }
    }
}
