namespace LearnMeAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
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
