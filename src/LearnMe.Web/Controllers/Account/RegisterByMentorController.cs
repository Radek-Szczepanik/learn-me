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
using System;


namespace LearnMe.Web.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterByMentorController : ControllerBase
    {
        private readonly SignInManager<UserBasic> _signInManager;
        private readonly UserManager<UserBasic> _userManager;
        private readonly ILogger<RegisterController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private static readonly Random Random = new Random();

        public RegisterByMentorController(
                UserManager<UserBasic> userManager,
                SignInManager<UserBasic> signInManager,
                ILogger<RegisterController> logger,
                IEmailSender emailSender,
                IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<RegisterFromMentor>> OnPostAsync(RegisterFromMentor input)
        {
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            var user = _mapper.Map<UserBasic>(input);
            var code = PasswordGenerator();
            var result = await _userManager.CreateAsync(user, code);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("Register", "api", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink + "pass:" + code, null);
                await _emailSender.SendEmailAsync(message);
                await _userManager.AddToRoleAsync(user, "Student");
                return Ok();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password", error.Description);
            }
            return Unauthorized(ModelState);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token1 = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (user == null)
                return Ok("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token1);
            return Ok(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        private static string PasswordGenerator()
        {
            int seed = Random.Next(1, int.MaxValue);
            //const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string specialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";
            const string digitChar = "0123456789";

            int passwordLength = 12;
            bool strongPassword = true;     
            var chars = new char[passwordLength];
            var rd = new Random(seed);

            for (var i = 0; i < passwordLength; i++)
            {
                // If we are to use special characters
                if (strongPassword && i % Random.Next(3, passwordLength) == 0)
                {
                    chars[i] = specialCharacters[rd.Next(0, specialCharacters.Length)];
                }
                else if (strongPassword && i % Random.Next(3, passwordLength) == 1)
                {
                    chars[i] = digitChar[rd.Next(0, digitChar.Length)];
                }
                else
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
            }

            return new string(chars);
        }
    }
}








