using Employees.Shared.Responses;

namespace Employees.Backend.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ActionResponse<IEnumerable<T>>> GetAsync();     
        Task<ActionResponse<T>> GetAsync(int id);             
        Task<ActionResponse<T>> AddAsync(T entity);           
        Task<ActionResponse<T>> UpdateAsync(T entity);        
        Task<ActionResponse<T>> DeleteAsync(int id);        
    }
}
