using System.ComponentModel.DataAnnotations;


namespace LearnMe.Models.Base
{
    public abstract class BaseHome : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        public string FileString { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Text { get; set; }

    }
}
