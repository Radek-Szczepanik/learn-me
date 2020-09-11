using System.ComponentModel.DataAnnotations;

namespace LearnMeAPI.DTOs
{
    public class UserForRegister
    {
        [Required(ErrorMessage ="Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Postcode { get; set; }
        public string City { get; set; }
        public int Telephone { get; set; }
        public string Email { get; set; }
        public int NIP { get; set; }
        public int PESEL { get; set; }
    }
}
