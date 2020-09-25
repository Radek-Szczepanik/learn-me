using Microsoft.EntityFrameworkCore;

namespace LearnMe.Infrastructure.Data
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

        public DbSet<UserBasic> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInvoiceData> UserInvoiceDatas { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRegistration> UserRegistrations { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
