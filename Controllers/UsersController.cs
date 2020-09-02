using LearnMeAPI.Data;
using LearnMeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LearnMeAPI.Controllers
{
    // http://localhost:5000

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);      // Ok zwraca z serwera status 200, że wszystko jest dobrze.
        }

        // GET api/value
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/value
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] User user)
        {
            var data = await _context.Users.FindAsync(id);
            data.FirstName = user.FirstName;
            _context.Users.Update(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }

        // DELETE api/value
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var data = await _context.Users.FindAsync(id);

            if (data == null)
                return NoContent();

            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}
