using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using LearnMe.Enum;


namespace LearnMe.Models.Domains.Users
{
    [NotMapped]
    public class UserRegistration : BaseUser
    {
        [Required, Compare(nameof(Password), ErrorMessage = "Password don't match, please try again")]
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
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
    }
}
