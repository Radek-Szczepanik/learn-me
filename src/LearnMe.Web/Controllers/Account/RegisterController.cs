﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Account;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;


namespace LearnMe.Web.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {


        private readonly SignInManager<UserBasic> _signInManager;
        private readonly UserManager<UserBasic> _userManager;
        private readonly ILogger<RegisterController> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterController(
                UserManager<UserBasic> userManager,
                SignInManager<UserBasic> signInManager,
                ILogger<RegisterController> logger,
                IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }   
        public RegisterDto Input;
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    ReturnUrl = returnUrl;
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //}

        [HttpPost]
        public async Task<ActionResult<RegisterDto>> OnPostAsync(RegisterDto Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var user = new UserBasic { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

            //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //    var callbackUrl = Url.Page(
            //        "/Account/ConfirmEmail",
            //        pageHandler: null,
            //        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
            //        protocol: Request.Scheme);

            //    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
            //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            //    if (_userManager.Options.SignIn.RequireConfirmedAccount)
            //    {
            //        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
            //    }
            //    else
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        return LocalRedirect(returnUrl);
            //    }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Ok(result);
        }   
       
    }
}



              




