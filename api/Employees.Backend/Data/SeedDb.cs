using Employees.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace Employees.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedGeoAsync();
            await TopUpEmployeesAsync();
            await _context.SaveChangesAsync();
        }

        private async Task SeedGeoAsync()
        {
            if (await _context.Countries.AnyAsync())
                return;

            var colombia = new Countries { Name = "Colombia" };
            var antioquia = new State { Name = "Antioquia", Country = colombia };
            var cundinamarca = new State { Name = "Cundinamarca", Country = colombia };

            var medellin = new City { Name = "Medellín", State = antioquia };
            var envigado = new City { Name = "Envigado", State = antioquia };
            var bogota = new City { Name = "Bogotá", State = cundinamarca };

            _context.Countries.Add(colombia);
            _context.States.AddRange(antioquia, cundinamarca);
            _context.Cities.AddRange(medellin, envigado, bogota);

            await _context.SaveChangesAsync();
        }

        private async Task TopUpEmployeesAsync()
        {
            var current = await _context.Employees.CountAsync();
            if (current >= 50) return;

            var toCreate = 50 - current;

            string[] firstNames = { "Juan", "Julian", "Ana", "Carlos", "Maria", "Laura", "Pedro", "Sofia", "Daniel", "Camila", "Luis", "Paula", "Jorge", "Valentina", "Andres", "Diana", "Santiago", "Carolina", "Felipe", "Andrea" };
            string[] lastNames = { "Gomez", "Perez", "Rodriguez", "Martinez", "Garcia", "Lopez", "Hernandez", "Ramirez", "Sanchez", "Torres", "Diaz", "Vargas", "Castro", "Moreno", "Ruiz", "Ortega", "Rojas", "Navarro", "Cortes", "Suarez" };

            var rnd = new Random();
            var list = new List<Employee>();

            for (int i = 0; i < toCreate; i++)
            {
                var fn = firstNames[rnd.Next(firstNames.Length)];
                var ln = lastNames[rnd.Next(lastNames.Length)];

                list.Add(new Employee
                {
                    FirstName = fn.Length > 30 ? fn[..30] : fn,
                    LastName = ln.Length > 30 ? ln[..30] : ln,
                    IsActive = rnd.Next(0, 2) == 1,
                    HireDate = DateTime.Now.AddDays(-rnd.Next(30, 3650)),
                    Salary = 1_000_000m + rnd.Next(0, 9) * 500_000m
                });
            }

            _context.Employees.AddRange(list);
        }
    }
}
