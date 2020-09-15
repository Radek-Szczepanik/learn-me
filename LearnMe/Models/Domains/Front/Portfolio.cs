using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Front
{
    public class Portfolio : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        public string InfoText { get; set; }

        public string Photo { get; set; }
    }
}
