using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Data;
using LearnMe.Models.Domains.Users;
using AutoMapper;
using LearnMe.DTO;

namespace LearnMe.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBasicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserBasicsController(ApplicationDbContext context, IMapper mapper)  // wstrzyknięcie do kontrolera
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserBasics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBasic>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var usersToReturn = _mapper.Map<IEnumerable<UserBasicDto>>(users);     // dodanie mapowania
            return Ok(usersToReturn);
        }

        // GET: api/UserBasics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBasic>> GetUserBasic(int id)
        {
            var userBasic = await _context.Users.FindAsync(id);

            if (userBasic == null)
            {
                return NotFound();
            }

            var userToReturn = _mapper.Map<UserBasicDto>(userBasic);       // dodanie mapowania

            return Ok(userToReturn);
        }

        // PUT: api/UserBasics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id, [FromBody] UserBasic userBasic)
        {
            var data = await _context.Users.FindAsync(id);

            if (data == null)
                return NotFound();

            data.FirstName = userBasic.FirstName;
            data.LastName = userBasic.LastName;
            data.PhoneNumber = userBasic.PhoneNumber;
            data.Address = userBasic.Address;
            data.Postcode = userBasic.Postcode;
            data.RegistrationDate = userBasic.RegistrationDate;
            data.Role = userBasic.Role;
            data.Status = userBasic.Status;
            data.UserGroup = userBasic.UserGroup;
            data.Notes = userBasic.Notes;
            data.Password = userBasic.Password;
            data.Email = userBasic.Email;

            _context.Users.Update(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }

        // POST: api/UserBasics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserBasic>> PostUserBasic(UserBasic userBasic)
        {
            _context.Users.Add(userBasic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserBasic", new { id = userBasic.Id }, userBasic);
        }

        // DELETE: api/UserBasics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserBasic>> DeleteUserBasic(int id)
        {
            var userBasic = await _context.Users.FindAsync(id);
            if (userBasic == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userBasic);
            await _context.SaveChangesAsync();

            return userBasic;
        }

        public bool UserBasicExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
