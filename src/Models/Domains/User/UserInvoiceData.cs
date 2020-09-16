using LearnMe.Models.Base;

namespace LearnMe.Models.Domains.Users
{
    public class UserInvoiceData : BaseEntity
    {
        public int UserId { get; set; }
        public UserBasic User { get; set; }
    }
}
