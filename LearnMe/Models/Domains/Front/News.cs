using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Front
{
    /// <summary>
    /// News class relates to blog-like posts on the website
    /// </summary>
    public class News : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string BlogPostHeader { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string BlogPostText { get; set; }

        public string Photo { get; set; }
    }
}
