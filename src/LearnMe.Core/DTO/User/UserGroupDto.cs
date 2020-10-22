using System.ComponentModel.DataAnnotations;

namespace LearnMe.Core.DTO.User
{
    public class UserGroupDto
    {
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Name { get; set; }

        public string OptionalDesription { get; set; }

    }
}
