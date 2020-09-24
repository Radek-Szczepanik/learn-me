using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMe.DTO.User
{
    public class UserLoginDto
    {
        [NotMapped]
        public class UserLogin
        {
            [Required(ErrorMessage = "This field is required")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
