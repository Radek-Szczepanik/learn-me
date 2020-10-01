using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Core.Interfaces;
using LearnMe.Core.DTO.User;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using LearnMe.Core.DTO.Config;
using LearnMe.Core.Interfaces.DTO;


namespace LearnMe.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBasicsController : ControllerBase
    {
           private readonly ICrudRepository<UserRegistrationDto> _crudRepositoryRegistration;
           private readonly ICrudRepository<UserBasicDto> _crudRepositoryBasic;


        public UserBasicsController(ICrudRepository<UserRegistrationDto> crudRepositoryRegistration,
            ICrudRepository<UserBasicDto> crudRepositoryBasic)
            {
                _crudRepositoryRegistration = crudRepositoryRegistration;
                _crudRepositoryBasic = crudRepositoryBasic;


            }

        //// GET: api/News
        //[HttpGet]
        //public async Task<IEnumerable<UserBasicDto>> GetNews()
        //{   
        //       return await _crudRepository.GetAllAsync(10, 1);
        //}


        // GET: api/UserBasics
        [HttpGet]
        public async Task<IEnumerable<UserBasicDto>> GetUsers()
        {
            return await _crudRepositoryBasic.GetAllAsync(5, 1);
        }

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


        [HttpPost]
        public async Task<bool> AddUser(UserRegistrationDto user)
        {
            //_mapper.Map<UserBasic>(user);
            await _crudRepositoryRegistration.InsertAsync(user);
            return true; 
        }

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

