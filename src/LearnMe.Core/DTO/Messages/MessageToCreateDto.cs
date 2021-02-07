using System;

namespace LearnMe.Core.DTO.Messages
{
    public class MessageToCreateDto
    {
        public string SenderId { get; set; }

        public string SenderEmail { get; set; }

        public string RecipientId { get; set; }

        public string RecipientEmail { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public MessageToCreateDto()
        {
            DateSent = DateTime.Now;
        }
    }
}
