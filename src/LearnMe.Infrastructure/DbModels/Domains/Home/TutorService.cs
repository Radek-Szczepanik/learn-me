using System.ComponentModel.DataAnnotations;
using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Infrastructure.Enum;

namespace LearnMe.Infrastructure.DbModels.Domains.Home
{
    /// <summary>
    /// Service class relates to courses, translations and other services in the offer
    /// </summary>
    public class TutorService : BaseHome
    {
        [Required(ErrorMessage = "This field is required")]
        public ServiceType Type { get; set; }
        public bool IsAvailable { get; set; }
    }
}
