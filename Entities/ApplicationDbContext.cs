using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeskBookingAPI.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString = "Server=RADEK;Database=DeskBookingDb;Trusted_Connection=True";

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
