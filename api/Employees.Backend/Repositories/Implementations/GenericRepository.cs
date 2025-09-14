
    using Employees.Backend.Data;
    using Employees.Backend.Repositories.Interfaces;
    using Employees.Shared.Responses;
    using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Repositories.Implementations;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        protected readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
            => new() { WasSuccess = true, Result = await _entity.ToListAsync() };

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            return row is null
                ? new() { WasSuccess = false, Message = "Record not found" }
                : new() { WasSuccess = true, Result = row };
        }

        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new() { WasSuccess = true, Result = entity };
            }
            catch (DbUpdateException)
            {
                return new() { WasSuccess = false, Message = "Duplicate or constraint error" };
            }
            catch (Exception ex)
            {
                return new() { WasSuccess = false, Message = ex.Message };
            }
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return new() { WasSuccess = true, Result = entity };
            }
            catch (DbUpdateException)
            {
                return new() { WasSuccess = false, Message = "Conflict updating record" };
            }
            catch (Exception ex)
            {
                return new() { WasSuccess = false, Message = ex.Message };
            }
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row is null) return new() { WasSuccess = false, Message = "Record not found" };

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new() { WasSuccess = true };
            }
            catch
            {
                return new() { WasSuccess = false, Message = "Cannot delete due to related data" };
            }
        }
    }


