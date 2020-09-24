﻿using LearnMe.Infrastructure.Enum;
using LearnMe.Infrastructure.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearnMe.Infrastructure.Models.Domains.Invoice;
using LearnMe.Infrastructure.Models.Domains.Lessons;

namespace LearnMe.Infrastructure.Models.Domains.Users
{
    public class UserBasic : BaseUser
    {
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
              
        //[Required(ErrorMessage = "This field is required")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PostalCode)]
        public int Postcode { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public UserGroup UserGroup { get; set; } // to indicate group of students

        public string Notes { get; set; }

        public IList<InvoiceBasic> InvoicesList { get; set; }

        public IList<UserLesson> UserLessons { get; set; }

        public UserInvoiceData InvoiceData { get; set; }

    }
}
