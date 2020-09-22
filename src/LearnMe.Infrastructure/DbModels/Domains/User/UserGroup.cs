using System.Collections.Generic;
using LearnMe.Infrastructure.DbModels.Base;

namespace LearnMe.Infrastructure.DbModels.Domains.User
{
    public class UserGroup : BaseUser
    {
        public string Name { get; set; }

        public string OptionalDesription { get; set; }

        public IList<UserBasic> UsersList { get; set; }
    }
}
