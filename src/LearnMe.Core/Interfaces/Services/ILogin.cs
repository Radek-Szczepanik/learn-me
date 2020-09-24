using System;
using System.Collections.Generic;
using System.Text;

namespace LearnMe.Core.Interfaces
{
    public interface ILogin
    {
        string GetHash(string password);
        bool Logout();



    }
}
