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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LearnMe.Controllers.Users
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserBasicsController : ControllerBase
    {
        private readonly UserManager<UserBasic> _userManager;
        private readonly IMapper _mapper;

        public UserBasicsController(
            UserManager<UserBasic> userManager,
            IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents(string rolename)
        {
            var role = await _userManager.GetUsersInRoleAsync(rolename);
            var user = _mapper.Map<IList<UserForMentorDto>>(role);

            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(UserBasic user)
        {
            var role = await _userManager.DeleteAsync(user);
            return Ok();
        }

   
        [HttpPut("{id}")]
         public async Task<ActionResult> PutUser(UserBasic user)
        {
            var role = await _userManager.UpdateAsync(user);
            return Ok();
        }

         [HttpPost]
         public async Task<ActionResult> CreateUser(UserBasic user)
        {
            var role = await _userManager.CreateAsync(user);
            return Ok();
        }
    }
}