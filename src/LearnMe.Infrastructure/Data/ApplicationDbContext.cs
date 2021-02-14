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
        public DbSet<HomeworkType> HomeworkTypes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<UserLessonHomework> UserLessonHomeworks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserBasic> UserBasic { get; set; }
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
    }
}
