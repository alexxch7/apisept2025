using Employees.Backend.Entities;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Countries>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<State>()
                .HasIndex(x => new { x.CountryId, x.Name })
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(x => new { x.StateId, x.Name })
                .IsUnique();

            modelBuilder.Entity<State>()
                .HasOne(x => x.Country)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasOne(x => x.State)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
