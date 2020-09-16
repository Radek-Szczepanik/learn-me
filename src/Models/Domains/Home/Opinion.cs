using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Home
{
    public class Opinion : BaseHome
   
        {
        [Required(ErrorMessage = "This field is required")]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
