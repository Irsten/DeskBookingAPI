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
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired();

            modelBuilder.Entity<Desk>()
                .Property(d => d.RoomId)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.BookingDate)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ExpirationDate)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.DeskId)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.EmployeeId)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
