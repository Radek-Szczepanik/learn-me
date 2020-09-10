using LearnMe.Enum;
using LearnMe.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMe.Models.Domains.AdminDomain
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required, Compare(nameof(Password), ErrorMessage = "Password don't match, please try again")]
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PostalCode)]
        public int Postcode { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public string Group { get; set; } // to indicate group of students - possibly use enum?

        public string Notes { get; set; }
    }
}
