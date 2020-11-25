using LearnMe.Infrastructure.Models.Base;
using LearnMe.Infrastructure.Models.Domains.Users;
using System;

namespace LearnMe.Infrastructure.Models.Domains.Messages
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }

        public UserBasic Sender { get; set; }

        public int RecipientId { get; set; }

        public UserBasic Recipient { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime DateSent { get; set; }

        public bool SenderDeleted { get; set; }

        public bool RecipientDeleted { get; set; }
    }
}
