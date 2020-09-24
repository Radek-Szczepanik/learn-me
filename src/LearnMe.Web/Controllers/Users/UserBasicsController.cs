using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.DTO;
using LearnMe.Core.Interfaces;


namespace LearnMe.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBasicsController : ControllerBase
    {
           private readonly ICrudRepository<UserBasicDto> _crudRepository;

            public UserBasicsController(ICrudRepository<UserBasicDto> crudRepository)
            {
                _crudRepository = crudRepository;
            }

            // GET: api/News
            [HttpGet]
            public async Task<IEnumerable<UserBasicDto>> GetNews()
            {
                return await _crudRepository.GetAll();
            }

            //private readonly ApplicationDbContext _context;

            //public UserBasicsController(ApplicationDbContext context)
            //{
            //    _context = context;
            //}

            //// GET: api/UserBasics
            //[HttpGet]
            //public async Task<ActionResult<IEnumerable<UserBasic>>> GetUsers()
            //{
            //    return await _context.Users.ToListAsync();
            //}

            //// GET: api/UserBasics/5
            //[HttpGet("{id}")]
            //public async Task<ActionResult<UserBasic>> GetUserBasic(int id)
            //{
            //    var userBasic = await _context.Users.FindAsync(id);

            //    if (userBasic == null)
            //    {
            //        return NotFound();
            //    }

            //    return userBasic;
            //}

            //// PUT: api/UserBasics/5
            //// To protect from overposting attacks, enable the specific properties you want to bind to, for
            //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            //[HttpPut("{id}")]
            //public async Task<IActionResult> PutUserBasic(int id, UserBasic userBasic)
            //{
            //    if (id != userBasic.Id)
            //    {
            //        return BadRequest();
            //    }

            //    _context.Entry(userBasic).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!UserBasicExists(id))
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

            //// POST: api/UserBasics
            //// To protect from overposting attacks, enable the specific properties you want to bind to, for
            //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            //[HttpPost]
            //public async Task<ActionResult<UserBasic>> PostUserBasic(UserBasic userBasic)
            //{
            //    _context.Users.Add(userBasic);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetUserBasic", new { id = userBasic.Id }, userBasic);
            //}

            //// DELETE: api/UserBasics/5
            //[HttpDelete("{id}")]
            //public async Task<ActionResult<UserBasic>> DeleteUserBasic(int id)
            //{
            //    var userBasic = await _context.Users.FindAsync(id);
            //    if (userBasic == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.Users.Remove(userBasic);
            //    await _context.SaveChangesAsync();

            //    return userBasic;
            //}

            //private bool UserBasicExists(int id)
            //{
            //    return _context.Users.Any(e => e.Id == id);
            //}
        }
    }

