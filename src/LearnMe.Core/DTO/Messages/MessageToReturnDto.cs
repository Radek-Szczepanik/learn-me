using LearnMe.Infrastructure.Models.Domains.Users;
using System;

namespace LearnMe.Core.DTO.Messages
{
    public class MessageToReturnDto
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public string SenderEmail { get; set; }

        public string RecipientId { get; set; }

        public string RecipientFirstName { get; set; }

        public string RecipientLastName { get; set; }

        public string RecipientEmail { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime DateSent { get; set; }

        public string MessageContainer { get; set; }
    }
}
