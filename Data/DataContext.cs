using LearnMeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnMeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
