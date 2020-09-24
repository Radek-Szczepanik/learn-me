using LearnMe.Models.Domains.Users;

namespace LearnMe.DTO.User
{
    public class UserInvoiceDataDto
    {
        public int UserId { get; set; }
        public UserBasic User { get; set; }
    }
}
