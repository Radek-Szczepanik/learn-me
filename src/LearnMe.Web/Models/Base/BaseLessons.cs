using System.ComponentModel.DataAnnotations;


namespace LearnMe.Models.Base
{
    public abstract class BaseLessons : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string FileString { get; set; }
    }
}
