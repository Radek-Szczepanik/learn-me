using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Account;
using LearnMe.Core.DTO.User;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LearnMe.Web.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<UserBasic> _signInManager;
        private readonly UserManager<UserBasic> _userManager;
        private readonly IUserClaimsPrincipalFactory<UserBasic> _addClaim;
        private readonly ILogger<LoginController> _logger;


        public LoginController(SignInManager<UserBasic> signInManager,
            ILogger<LoginController> logger,
            UserManager<UserBasic> userManager,
            IUserClaimsPrincipalFactory<UserBasic> addClaim)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _addClaim = addClaim;


        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(LoginDto input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                await _addClaim.CreateAsync(await _userManager.FindByEmailAsync(input.Email));
                _logger.LogInformation("User logged in.");
                return Ok();
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Unauthorized");
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
           return Ok();
        }
    }
}

