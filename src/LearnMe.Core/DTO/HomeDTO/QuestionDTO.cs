using LearnMe.Infrastructure.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Core.DTO.HomeDTO
{
    class QuestionDTO : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string AnswerText { get; set; }

        public int KeywordOrGroupIdentifier { get; set; }
    }
}
