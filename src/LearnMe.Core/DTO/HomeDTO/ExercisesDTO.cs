using System.ComponentModel.DataAnnotations;

namespace LearnMe.Core.DTO.HomeDTO
{
    public class ExercisesDTO
    {
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }
    }
}
