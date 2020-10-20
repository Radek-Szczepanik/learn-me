using LearnMe.Infrastructure.Models.Base;
using LearnMe.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Core.DTO.HomeDTO
{
    class TutorServiceDTO : BaseHome
    {
        [Required(ErrorMessage = "This field is required")]
        public ServiceType Type { get; set; }
        public bool IsAvailable { get; set; }
    }
}
