using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Base;

namespace LearnMe.Infrastructure.Models.Domains.Users
{
    public class UserGroup : BaseUser
    {
        public string Name { get; set; }

        public string OptionalDesription { get; set; }

        public IList<UserBasic> UsersList { get; set; }
    }
}
