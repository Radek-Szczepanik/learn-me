using System.Linq;
using Microsoft.EntityFrameworkCore;
using LearnMe.Models.Domains.Calendar;
using LearnMe.Models.Domains.Front;
using LearnMe.Models.Domains.Invoice;
using LearnMe.Models.Domains.Lessons;
using LearnMe.Models.Domains.Mail;
using LearnMe.Models.Domains.Shop;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TutorService> TutorServices { get; set; }

        public DbSet<InvoiceBasic> InvoiceBasics { get; set; }

        public DbSet<Correction> Corrections { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<UserLessonHomework> UserLessonHomeworks { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInvoiceData> UserInvoiceDatas { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRegistration> UserRegistrations { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<UserLesson>()
            .HasKey(ul => ul.Id);

            modelbuilder.Entity<User>()
                .HasOne(u => u.InvoiceData)
                .WithOne(invD => invD.User)
                .HasForeignKey<UserInvoiceData>(uInvD => uInvD.UserId);

            modelbuilder.Entity<User>()
                .HasOne(u => u.Login)
                .WithOne(ul => ul.User)
                .HasForeignKey<UserLogin>(ul=> ul.UserId);

            modelbuilder.Entity<User>()
                .HasOne(u => u.Registration)
                .WithOne(ur => ur.User)
                .HasForeignKey<UserRegistration>(ur => ur.UserId);

        }
    }
}
