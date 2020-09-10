using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Enum;

namespace LearnMe.Models.Domains.HomeDomain
{
    /// <summary>
    /// Exercise class relates to eg. language level test files available for not-logged users
    /// </summary>
    public class Exercise : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public ExerciseGroup ExerciseGroup { get; set; }

        public string FileName { get; set; }
    }
}
