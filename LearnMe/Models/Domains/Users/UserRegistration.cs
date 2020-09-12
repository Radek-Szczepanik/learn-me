using LearnMe.Models.Base;

namespace LearnMe.Models.Domains.Users
{
    public class UserRegistration : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
