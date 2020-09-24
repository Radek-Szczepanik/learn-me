using LearnMe.Infrastructure.Models.Base;

namespace LearnMe.Infrastructure.Models.Domains.Users
{
    public class UserInvoiceData : BaseEntity
    {
        public int UserId { get; set; }
        public UserBasic User { get; set; }
    }
}
