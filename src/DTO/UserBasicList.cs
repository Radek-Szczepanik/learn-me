using System.ComponentModel.DataAnnotations;

namespace LearnMe.DTO
{
    public class UserBasicList
    {
        public int Id { get; set; }

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
    }
}
