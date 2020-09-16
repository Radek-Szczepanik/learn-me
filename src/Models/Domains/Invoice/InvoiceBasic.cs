﻿using System;
using LearnMe.Models.Base;
using System.ComponentModel.DataAnnotations;
using LearnMe.Enum;
using System.Collections.Generic;
using LearnMe.Models.Domains.Lessons;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Models.Domains.Invoice
{
    public class InvoiceBasic : BaseUser
    {
        public int StudentId { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public int NumberOfHours { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public int SumToPayInPLN { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        public PaymentAction PaymentAction { get; set; }

        [Required(ErrorMessage = "This field is required")]
        // TODO Add string length attribute = Invoice number length
        public string InvoiceNumber { get; set; }

        public string InvoiceFile { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public User Student { get; set; }

        public IList<Lesson> Lessons { get; set; }
    }
}
