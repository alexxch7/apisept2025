using Employees.Backend.Data;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.Entities;
using Employees.Shared.Responses;
using Employees.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Repositories.Implementations
{
    public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        private readonly DataContext _context;

        public EmployeesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ActionResponse<IEnumerable<Employee>>> SearchByNameAsync(string term)
        {
            term = term?.Trim().ToLower() ?? string.Empty;

            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(term) ||
                    e.LastName.ToLower().Contains(term));

            var list = await query
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Employee>> { WasSuccess = true, Result = list };
        }

        public async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO dto)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Filter))
            {
                var f = dto.Filter.Trim().ToLower();
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(f) ||
                    e.LastName.ToLower().Contains(f));
            }

            var list = await query
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Skip((dto.Page - 1) * dto.RecordsNumber)
                .Take(dto.RecordsNumber)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Employee>> { WasSuccess = true, Result = list };
        }

        public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO dto)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Filter))
            {
                var f = dto.Filter.Trim().ToLower();
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(f) ||
                    e.LastName.ToLower().Contains(f));
            }

            var total = await query.CountAsync();
            return new ActionResponse<int> { WasSuccess = true, Result = total };
        }
    }
}
