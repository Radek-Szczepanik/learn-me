using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Enum;

namespace LearnMe.Models.Domains.Front
{
    /// <summary>
    /// Service class relates to courses, translations and other services in the offer
    /// </summary>
    public class TutorService : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public ServiceType Type { get; set; }

        public string InfoText { get; set; }

        public string Photo { get; set; }

        public bool IsAvailable { get; set; }
    }
}
