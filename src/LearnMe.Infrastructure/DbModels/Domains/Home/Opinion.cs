using System.ComponentModel.DataAnnotations;
using LearnMe.Infrastructure.DbModels.Base;

namespace LearnMe.Infrastructure.DbModels.Domains.Home
{
    public class Opinion : BaseHome
   
        {
        [Required(ErrorMessage = "This field is required")]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
