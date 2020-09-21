using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Passport> Passports { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Passport)
                .WithOne(p => p.Employee)
                .HasForeignKey<Passport>(p => p.Id);
        }
    }
}
