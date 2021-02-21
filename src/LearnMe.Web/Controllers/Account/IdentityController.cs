using Microsoft.AspNetCore.Mvc;
using LearnMe.Infrastructure.Models.Domains.Users;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace LearnMe.Web.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
           
        [HttpGet]
        public string[] GetIdentity()
        {   
            var identityString = new string[5];
            identityString[0] = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
            identityString[1] = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
            identityString[2] = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Surname)?.Value;
            identityString[3] = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.GivenName)?.Value;
            identityString[4] = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.HomePhone)?.Value;

            return identityString;
        }

    }

}
