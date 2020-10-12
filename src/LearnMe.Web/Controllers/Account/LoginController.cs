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
        private readonly UserManager<UserBasic> _userManager;
        private readonly SignInManager<UserBasic> _signInManager;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;


        public LoginController(SignInManager<UserBasic> signInManager,
            ILogger<LoginController> logger,
            UserManager<UserBasic> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;

        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> OnPostAsync(LoginDto input, string returnUrl = null)
        {
            //returnUrl = returnUrl ?? Url.Content("~/");

                var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                return Ok("User logged in.");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return NotFound();

                }
        }

        [HttpOptions]
        public bool Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //TODO: Redirect to Home
            return true;
        }
    }
}

