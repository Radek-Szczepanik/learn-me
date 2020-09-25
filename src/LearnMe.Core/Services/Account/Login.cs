using System;
using System.Collections.Generic;
using System.Text;
using LearnMe.Core.Interfaces;

namespace LearnMe.Core.Services.Account
{
    class Login : ILogin
    {
        public string GetHash(string password)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
