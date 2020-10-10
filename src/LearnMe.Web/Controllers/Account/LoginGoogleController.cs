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
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [HttpOptions]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return Ok();

            //var result = await signInManager.ExternalLoginSignInAsync(info.Principal, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };

            var usera = userManager.FindByEmailAsync(userInfo[1]);
            //usera.
          //  var result = signInManager.PasswordSignInAsync(usera.)
            

            if (usera == null)
                return Ok(userInfo);
            else
            {
                UserBasic user = new UserBasic
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return Ok();
                    }
                }
                return Ok();
            }
        }
    }
}
