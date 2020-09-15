﻿using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace LearnMe.Models.Domains.Front
{
    /// <summary>
    /// Question class relates to FAQ questions and answers
    /// </summary>
    public class Question : BaseEntity
    {
        [Required(ErrorMessage = "This field is required")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string AnswerText { get; set; }

        public int KeywordOrGroupIdentifier { get; set; }
    }
}
