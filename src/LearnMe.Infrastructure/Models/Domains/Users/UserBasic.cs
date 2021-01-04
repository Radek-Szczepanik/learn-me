using LearnMe.Shared.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearnMe.Infrastructure.Models.Domains.Invoice;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using Microsoft.AspNetCore.Identity;
using LearnMe.Infrastructure.Models.Domains.Messages;

namespace LearnMe.Infrastructure.Models.Domains.Users
{
    public class UserBasic : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostCode { get; set; }
        public string ImgPath { get; set;}
        public UserStatus Status { get; set; }
        public UserGroup UserGroup { get; set; } // to indicate group of students
        public string Notes { get; set; }
        public IList<InvoiceBasic> InvoicesList { get; set; }
        public IList<UserLesson> UserLessons { get; set; }
        public UserInvoiceData InvoiceData { get; set; }
        public IList<Message> MessagesSent { get; set; }
        public IList<Message> MessagesReceived { get; set; }
    }
}
