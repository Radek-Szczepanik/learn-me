﻿// <auto-generated />
using System;
using LearnMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LearnMe.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Calendar.CalendarEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CalendarEvents");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Home.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ExerciseGroup")
                        .HasColumnType("int");

                    b.Property<string>("FileString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Home.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Home.Opinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Opinions");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Home.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Home.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KeywordOrGroupIdentifier")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Home.TutorService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TutorServices");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Invoice.InvoiceBasic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfHours")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentAction")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SumToPayInPLN")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("InvoiceBasics");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Correction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Corrections");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserLessonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserLessonId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CalendarEventId")
                        .HasColumnType("int");

                    b.Property<int>("LessonStatus")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedInvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CalendarEventId")
                        .IsUnique();

                    b.HasIndex("RelatedInvoiceId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.UserLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLessons");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.UserLessonHomework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CorrectionId")
                        .HasColumnType("int");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<int>("UserLessonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CorrectionId")
                        .IsUnique();

                    b.HasIndex("HomeworkId");

                    b.HasIndex("UserLessonId");

                    b.ToTable("UserLessonHomeworks");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Mail.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MessageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Mail.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Shop.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("Postcode")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionalDesription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserInvoiceData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserInvoiceDatas");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Invoice.InvoiceBasic", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", "Student")
                        .WithMany("InvoicesList")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Homework", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Lessons.UserLesson", null)
                        .WithMany("Homeworks")
                        .HasForeignKey("UserLessonId");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Lesson", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Calendar.CalendarEvent", "CalendarEvent")
                        .WithOne("Lesson")
                        .HasForeignKey("LearnMe.Infrastructure.Models.Domains.Lessons.Lesson", "CalendarEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Invoice.InvoiceBasic", "RelatedInvoice")
                        .WithMany("Lessons")
                        .HasForeignKey("RelatedInvoiceId");

                    b.Navigation("CalendarEvent");

                    b.Navigation("RelatedInvoice");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.UserLesson", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Lessons.Lesson", "Lesson")
                        .WithMany("UserLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", "User")
                        .WithMany("UserLessons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.UserLessonHomework", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Lessons.Correction", "Correction")
                        .WithOne("UserLessonHomework")
                        .HasForeignKey("LearnMe.Infrastructure.Models.Domains.Lessons.UserLessonHomework", "CorrectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Lessons.Homework", "Homework")
                        .WithMany("UserLessonHomeworkList")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Lessons.UserLesson", "UserLesson")
                        .WithMany()
                        .HasForeignKey("UserLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Correction");

                    b.Navigation("Homework");

                    b.Navigation("UserLesson");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Mail.Attachment", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Mail.Message", "Message")
                        .WithMany("AttachedFiles")
                        .HasForeignKey("MessageId");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Mail.Message", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Users.UserGroup", "UserGroup")
                        .WithMany("UsersList")
                        .HasForeignKey("UserGroupId");

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserInvoiceData", b =>
                {
                    b.HasOne("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", "User")
                        .WithOne("InvoiceData")
                        .HasForeignKey("LearnMe.Infrastructure.Models.Domains.Users.UserInvoiceData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Calendar.CalendarEvent", b =>
                {
                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Invoice.InvoiceBasic", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Correction", b =>
                {
                    b.Navigation("UserLessonHomework");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Homework", b =>
                {
                    b.Navigation("UserLessonHomeworkList");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.Lesson", b =>
                {
                    b.Navigation("UserLessons");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Lessons.UserLesson", b =>
                {
                    b.Navigation("Homeworks");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Mail.Message", b =>
                {
                    b.Navigation("AttachedFiles");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserBasic", b =>
                {
                    b.Navigation("InvoiceData");

                    b.Navigation("InvoicesList");

                    b.Navigation("UserLessons");
                });

            modelBuilder.Entity("LearnMe.Infrastructure.Models.Domains.Users.UserGroup", b =>
                {
                    b.Navigation("UsersList");
                });
#pragma warning restore 612, 618
        }
    }
}
