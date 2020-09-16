using System.Collections.Generic;
using LearnMe.Models.Base;

namespace LearnMe.Models.Domains.Users
{
    public class UserGroup : BaseUser
    {
        public string Name { get; set; }

        public string OptionalDesription { get; set; }

        public IList<User> UsersList { get; set; }
    }
}
