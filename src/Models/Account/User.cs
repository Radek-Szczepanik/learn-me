﻿using System;
using System.Collections.Generic;
using LernMe.Models;
using LernMe.Models.Lesson;

namespace QuestStore3.Models
{
    public enum Status
    {
        Active, Inactive, Archival
    }
    public enum Role
    {
        Admin, Mentor, Student
    }

    public class User
    {
        public int UserId { get; set; }
        public string Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string PasswordHashed { get; set; }
        public string Adress { get; set; }
        public int Postcode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }

        //Relations to 
        public List<LessonsAssigned> Lessons { get; set; }
    }
}
