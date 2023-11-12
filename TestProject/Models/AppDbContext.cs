using Microsoft.EntityFrameworkCore;
using TestProject.Models.Entities;

namespace TestProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        
    }
}
