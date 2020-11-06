using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Infrastructure.Models.Domains.Home
{
    public class Opinion : BaseHome
    {
        [Required(ErrorMessage = "This field is required")]
        [Range(0, 5)]
        public int Rating { get; set; }

        public string Date { get; set; }

        public string Author { get; set; }
   }
}
