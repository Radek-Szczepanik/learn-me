using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Core.DTO.HomeDTO
{
    class OpinionDTO : BaseHome
    {
        [Required(ErrorMessage = "This field is required")]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
