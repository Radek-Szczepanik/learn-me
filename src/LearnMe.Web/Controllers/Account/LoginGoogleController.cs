using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LearnMe.Infrastructure.Models.Domains.Users;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace LearnMe.Web.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginGoogleController : ControllerBase
    {
        private UserManager<UserBasic> userManager;
        private SignInManager<UserBasic> signInManager;

        public LoginGoogleController(UserManager<UserBasic> userMgr, SignInManager<UserBasic> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }
 
        [HttpGet]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("", "response");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [HttpGet]
        [Route("/response")]

        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return Ok();

            var userInfo = info.Principal.FindFirst(ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(userInfo);

            if (user != null)
            {
                await signInManager.SignInAsync(user, false);
                return Ok();
            }

            return NotFound();
        }
    }
}
