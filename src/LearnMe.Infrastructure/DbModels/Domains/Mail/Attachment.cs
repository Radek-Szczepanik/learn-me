using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Models.Domains.Mail;

namespace LearnMe.Infrastructure.DbModels.Domains.Mail
{
    public class Attachment : BaseEntity
    {
        public string FileName { get; set; }

        public Message Message { get; set; }
    }
}
