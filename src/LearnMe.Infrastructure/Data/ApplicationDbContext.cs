﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Models.Domains.Invoice;
using LearnMe.Infrastructure.Models.Domains.Lessons;

using LearnMe.Infrastructure.Models.Domains.Shop;
using LearnMe.Infrastructure.Models.Domains.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LearnMe.Infrastructure.Models.Domains.Messages;

namespace LearnMe.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserBasic>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<CalendarSynchronization> CalendarSynchronizations { get; set; }

        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TutorService> TutorServices { get; set; }

        public DbSet<InvoiceBasic> InvoiceBasics { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Correction> Corrections { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<UserLessonHomework> UserLessonHomeworks { get; set; }
        public DbSet<Product> Products { get; set; }

        //public DbSet<UserBasic> UserBasic { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInvoiceData> UserInvoiceDatas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // każdy nadawca może wysłać wiele wiadomości
            modelBuilder.Entity<Message>().HasOne(u => u.Sender)
                                     .WithMany(m => m.MessagesSent)
                                     .HasForeignKey(e => e.SenderId)
                                     .OnDelete(DeleteBehavior.Restrict);


            // każdy odbiorca może otrzymać wiele wiadomości
            modelBuilder.Entity<Message>().HasOne(u => u.Recipient)
                                     .WithMany(m => m.MessagesReceived)
                                     .HasForeignKey(e => e.RecipientId)
                                     .OnDelete(DeleteBehavior.Restrict);
                                     

           

            
                                            


        }


        //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<UserLesson>()
        //    .HasKey(ul => ul.Id);

        //    modelBuilder.Entity<User>()
        //        .HasOne(u => u.InvoiceData)
        //        .WithOne(invD => invD.User)
        //        .HasForeignKey<UserInvoiceDataDto>(uInvD => uInvD.UserId);

        //modelBuilder.Entity<User>()
        //    .HasOne(u => u.Login)
        //    .WithOne(ul => ul.User)
        //    .HasForeignKey<UserLogin>(ul => ul.UserId);

        //modelBuilder.Entity<User>()
        //    .HasOne(u => u.Registration)
        //    .WithOne(ur => ur.User)
        //    .HasForeignKey<UserRegistration>(ur => ur.UserId);

        //// SEEDING DATA
        //modelBuilder.Entity<CalendarEvent>().HasData(
        //    new CalendarEvent()
        //    {
        //        Id = 1,
        //        Title = "A1 lesson",
        //        Description = "Animals vocabulary",
        //        Start = DateTime.UtcNow.AddDays(9).AddHours(2),
        //        End = DateTime.UtcNow.AddDays(9).AddHours(4),
        //        IsDone = false
        //    },
        //    new CalendarEvent()
        //    {
        //        Id = 2,
        //        Title = "C1 lesson",
        //        Description = "Future simple",
        //        Start = DateTime.UtcNow.AddDays(-9).AddHours(2),
        //        End = DateTime.UtcNow.AddDays(-9).AddHours(4),
        //        IsDone = true
        //    }
        //);

        //modelBuilder.Entity<Lesson>().HasData(
        //    new Lesson()
        //    {
        //        Id = 1,
        //        CalendarEventId = 1,
        //        Title = "Lesson 1",
        //        Text = "Please come",
        //        LessonStatus = LessonStatus.New,
        //    },
        //    new Lesson()
        //    {
        //        Id = 2,
        //        CalendarEventId = 2,
        //        Title = "Lesson 2",
        //        Text = "Please come",
        //        LessonStatus = LessonStatus.Done,
        //    },
        //    new Lesson()
        //    {
        //        Id = 3,
        //        CalendarEventId = 3,
        //        Title = "Lesson 3",
        //        MessageText = "Please come",
        //        LessonStatus = LessonStatus.New,
        //    }
        //);

        //modelBuilder.Entity<User>().HasData(
        //    new User()
        //    {
        //        Id = 1,
        //        FirstName = "Maciek",
        //        LastName = "Kowalski",
        //        Address = "Torun",
        //        Email = "maciek@gmail.com",
        //        RegistrationDate = DateTime.UtcNow,
        //        Password = "somePsw"
        //    },
        //    new User()
        //    {
        //        Id = 2,
        //        FirstName = "Anna",
        //        LastName = "Nowak",
        //        Address = "Torun",
        //        Email = "ania@gmail.com",
        //        RegistrationDate = DateTime.UtcNow.AddMinutes(3),
        //        Password = "somePsw"
        //    },
        //    new User()
        //    {
        //        Id = 3,
        //        FirstName = "Barbara",
        //        LastName = "Zamojska",
        //        Address = "Warszawa",
        //        Email = "maciek@gmail.com",
        //        RegistrationDate = DateTime.UtcNow.AddHours(2),
        //        Password = "somePsw"
        //    }
        //);

        //modelBuilder.Entity<UserLesson>().HasData(
        //    new UserLesson() { Id = 1, UserId = 1, LessonId = 1 },
        //    new UserLesson() { Id = 2, UserId = 2, LessonId = 2 },
        //    //new UserLesson() { Id = 3, UserId = 2, LessonId = 3 },
        //    new UserLesson() { Id = 4, UserId = 3, LessonId = 3 }, new UserLesson() { Id = 3, UserId = 3, LessonId = 3 }
        //);


    }
}
