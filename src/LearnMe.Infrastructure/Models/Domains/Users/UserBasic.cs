using LearnMe.Shared.Enum;
using LearnMe.Infrastructure.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearnMe.Infrastructure.Models.Domains.Invoice;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using Microsoft.AspNetCore.Identity;

namespace LearnMe.Infrastructure.Models.Domains.Users
{
    public class UserBasic : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostCode { get; set; }
        public UserStatus Status { get; set; }
        public UserGroup UserGroup { get; set; } // to indicate group of students
        public string Notes { get; set; }
        public IList<InvoiceBasic> InvoicesList { get; set; }
        public IList<UserLesson> UserLessons { get; set; }
        public UserInvoiceData InvoiceData { get; set; }
    }
}
