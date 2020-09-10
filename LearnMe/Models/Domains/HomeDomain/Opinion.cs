using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.HomeDomain
{
    public class Opinion : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Text { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
