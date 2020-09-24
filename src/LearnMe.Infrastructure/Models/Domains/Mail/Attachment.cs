using LearnMe.Infrastructure.Models.Base;

namespace LearnMe.Infrastructure.Models.Domains.Mail
{

    public class Attachment : BaseEntity
    {
        public string FileName { get; set; }

        public Message Message { get; set; }
    }
}
