
using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Infrastructure.Models.Domains.Lessons
{
    public class HomeworkType : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Type { get; set; }
    }
}

