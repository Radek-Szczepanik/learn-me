using LearnMe.Infrastructure.Models.Domains.Users;
using System;

namespace LearnMe.Core.DTO.Messages
{
    public class MessageToReturnDto
    {
        public int SenderId { get; set; }

        public string SenderName { get; set; }

        public int RecipientId { get; set; }

        public string RecipientName { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime DateSent { get; set; }
    }
}
