using Employees.Backend.Data;
using Employees.Backend.Helpers;
using Employees.Backend.Repositories.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            var list = await _context.Set<T>().AsNoTracking().ToListAsync();
            return new ActionResponse<IEnumerable<T>> { WasSuccess = true, Result = list };
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity is null
                ? new ActionResponse<T> { WasSuccess = false, Message = "Not found" }
                : new ActionResponse<T> { WasSuccess = true, Result = entity };
        }

        public virtual async Task<ActionResponse<T>> AddAsync(T model)
        {
            _context.Set<T>().Add(model);
            await _context.SaveChangesAsync();
            return new ActionResponse<T> { WasSuccess = true, Result = model };
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return new ActionResponse<T> { WasSuccess = true, Result = model };
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null)
                return new ActionResponse<T> { WasSuccess = false, Message = "Not found" };

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return new ActionResponse<T> { WasSuccess = true };
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
        {
            var query = _context.Set<T>().AsQueryable();
            var list = await query.Paginate(pagination).AsNoTracking().ToListAsync();
            return new ActionResponse<IEnumerable<T>> { WasSuccess = true, Result = list };
        }

        public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var query = _context.Set<T>().AsQueryable();
            var count = await query.CountAsync();
            return new ActionResponse<int> { WasSuccess = true, Result = count };
        }
    }
}
