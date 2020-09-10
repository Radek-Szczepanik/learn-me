using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LearnMe.Models.Domains.AdminDomain
{
    public class Message : BaseEntity
    {
        public User FromUser { get; set; }

        public User ToUser { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string MessageText { get; set; }


        public IList<string> AttachedFiles { get; set; }

        public IList<Message> RelatedMessages { get; set; } // message thread with one topic/to one student
    }
}
