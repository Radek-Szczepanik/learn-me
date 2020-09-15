using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Enum;

namespace LearnMe.Models.Domains.Home
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
