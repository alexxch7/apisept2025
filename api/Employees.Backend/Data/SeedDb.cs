using Employees.Shared.Entities;

namespace Employees.Backend.Data;

    public class SeedDb(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await SeedEmployeesAsync();
        }

        private async Task SeedEmployeesAsync()
        {
            if (_context.Employees.Any()) return;

            var list = new List<Employee>
        {
            new() { FirstName="Alexander",   LastName="Chavarría",     IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-3), Salary=2_500_000 },
            new() { FirstName="Esteban", LastName="Gómez",     IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-1), Salary=1_800_000 },
            new() { FirstName="Andrés",    LastName="Jimenez",     IsActive=false, HireDate=DateTime.UtcNow.AddYears(-5), Salary=3_200_000 },
            new() { FirstName="Julia",  LastName="Restrepo",  IsActive=true,  HireDate=DateTime.UtcNow.AddMonths(-8),Salary=1_200_000 },
            new() { FirstName="Andrés", LastName="Juarez",    IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-2), Salary=4_000_000 },
            new() { FirstName="Diana",  LastName="Santamaría",  IsActive=true,  HireDate=DateTime.UtcNow.AddMonths(-3),Salary=1_500_000 },
            new() { FirstName="Juan", LastName="Alvarez",    IsActive=false, HireDate=DateTime.UtcNow.AddYears(-6), Salary=2_000_000 },
            new() { FirstName="Laura",  LastName="Montoya",   IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-4), Salary=2_700_000 },
            new() { FirstName="Sofía",  LastName="Jiménez",   IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-1), Salary=1_900_000 },
            new() { FirstName="Julián", LastName="Arboleda",  IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-2), Salary=2_200_000 },
            new() { FirstName="José",   LastName="Sanabria",  IsActive=true,  HireDate=DateTime.UtcNow.AddYears(-7), Salary=5_000_000 },
        };

            _context.Employees.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
