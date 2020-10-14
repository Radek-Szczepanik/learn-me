using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Account;
using LearnMe.Core.Interfaces.Services;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace LearnMe.Web.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserManager<LoginDto> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordController(UserManager<LoginDto> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        //public async Task<IActionResult> OnPostAsync(LoginDto Input)
        //{
        //    var user = await _userManager.FindByEmailAsync(Input.Email);
        //    if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //    {
        //        return RedirectToPage("./ForgotPasswordConfirmation");
        //    }

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var confirmationLink = Url.Action("Register", "api", new { token, email = user.Email }, Request.Scheme);
        //    var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
        //    await _emailSender.SendEmailAsync(message);

        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //    var callbackUrl = Url.Page(
        //        "/Account/ResetPassword",
        //        pageHandler: null,
        //        values: new { area = "Identity", code },
        //        protocol: Request.Scheme);

        //    await _emailSender.SendEmailAsync(
        //        Input.Email,
        //        "Reset Password",
        //        $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //    return RedirectToPage("./ForgotPasswordConfirmation");

        //}
    }
}
