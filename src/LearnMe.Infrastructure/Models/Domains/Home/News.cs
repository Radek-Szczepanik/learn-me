using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Infrastructure.Models.Domains.Home
{
    /// <summary>
    /// News class relates to blog-like posts on the website
    /// </summary>
    public class News : BaseHome
    {
        public string ImgPath { get; set;}

    }
}
