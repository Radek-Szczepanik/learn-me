using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Account;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using LearnMe.Core.Services.Account.Email;
using LearnMe.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

 
namespace LearnMe.Web.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager, 
        UserManager<UserBasic> userManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(IdentityRole roleViewModel)
        {
            var roleresult = await _roleManager.CreateAsync(roleViewModel);
            return Ok();
        }
    }
}
