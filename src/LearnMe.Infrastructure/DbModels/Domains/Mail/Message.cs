using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Infrastructure.DbModels.Domains.Mail
{
    public class Message : BaseEntity
    {
        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public UserBasic FromUser { get; set; }

        public UserBasic ToUser { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string MessageText { get; set; }

        public IList<Attachment> AttachedFiles { get; set; }

        //public IList<Message> RelatedMessages { get; set; } // message thread with one topic/to one student
    }
}
