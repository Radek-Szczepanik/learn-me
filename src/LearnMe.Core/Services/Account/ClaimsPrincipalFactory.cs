using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LearnMe.Core.Services.Account
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserBasic, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<UserBasic> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserBasic user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.FirstName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.LastName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.HomePhone, user.ImgPath ?? ""));


            return identity;
        }
    }
}
