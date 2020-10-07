using System;
using System.Collections.Generic;
using System.Text;
using LearnMe.Core.DTO.User;
using LearnMe.Core.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMe.Shared.Enum;

namespace LearnMe.Core.Services.Account
{
    class LoginService : ILoginService
    {
        public UserBasicDto RegisterHashDateTime(UserBasicDto user)
        {
            var temp = user.Password.GetHashCode();
            user.Password = temp.ToString();
            user.RegistrationDate = DateTime.UtcNow;
            user.Status = UserStatus.Inactive;
            return user;
        }
           
    }
}
