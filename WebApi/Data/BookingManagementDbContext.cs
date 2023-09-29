using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) {  }

        // Add Models to Migrate
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //menambahkan constraint unique
            modelBuilder.Entity<Employee>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // 1 University has Many Educations
            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.University)
                .HasForeignKey(e => e.UniversityGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 educations has 1 Employee
            modelBuilder.Entity<Education>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Employee has 1 Accounts
            modelBuilder.Entity<Employee>()
                .HasOne(e=> e.Accounts)
                .WithOne(e => e.Employee)
                .HasForeignKey<Accounts>(e => e.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Accounts has Many Account Roles
            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.AccountRoles)
                .WithOne(e => e.Accounts)
                .HasForeignKey(e => e.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Roles has Many Accounts Role
            modelBuilder.Entity<Role>()
                .HasMany(e => e.AccountRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Employee has Many Bookings
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Bookings)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 Room has Many Bookings
            modelBuilder.Entity<Room>()
                .HasMany(e => e.Bookings)
                .WithOne(e => e.Room)
                .HasForeignKey(e => e.RoomGuid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
