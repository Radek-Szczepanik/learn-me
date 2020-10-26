using System;
using System.Collections.Generic;
using System.Text;
using LearnMe.Core.DTO.User;

namespace LearnMe.Core.Interfaces
{
    public interface ILoginService
    {
        UserBasicDto RegisterHashDateTime(UserBasicDto User);
    }
}
