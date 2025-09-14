using Employees.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Employee>().HasIndex(e => e.FirstName);
        modelBuilder.Entity<Employee>().HasIndex(e => e.LastName);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasPrecision(18, 2);
    }
}
