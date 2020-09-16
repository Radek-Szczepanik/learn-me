using LearnMe.Models.Base;

namespace LearnMe.Models.Domains.Mail
{
    public class Attachment : BaseEntity
    {
        public string FileName { get; set; }

        public Message Message { get; set; }
    }
}
