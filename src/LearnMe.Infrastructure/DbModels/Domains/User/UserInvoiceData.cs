using LearnMe.Infrastructure.DbModels.Base;

namespace LearnMe.Infrastructure.DbModels.Domains.User
{
    public class UserInvoiceData : BaseEntity
    {
        public int UserId { get; set; }
        public UserBasic User { get; set; }
    }
}
