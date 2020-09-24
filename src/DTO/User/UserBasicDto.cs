using LearnMe.Enum;
using LearnMe.Models.Domains.Invoice;
using LearnMe.Models.Domains.Lessons;
using LearnMe.Models.Domains.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.DTO
{
    public class UserBasicDto
    {
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PostalCode)]
        public int Postcode { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public UserGroup UserGroup { get; set; } // to indicate group of students

        public string Notes { get; set; }

        public IList<InvoiceBasic> InvoicesList { get; set; }

        public IList<UserLesson> UserLessons { get; set; }

        public UserInvoiceData InvoiceData { get; set; }
    }
}

