using Employees.Shared.DTOs;
using Employees.Shared.Responses;

namespace Employees.Backend.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ActionResponse<IEnumerable<T>>> GetAsync();
        Task<ActionResponse<T>> GetAsync(int id);
        Task<ActionResponse<T>> AddAsync(T model);
        Task<ActionResponse<T>> UpdateAsync(T model);
        Task<ActionResponse<T>> DeleteAsync(int id);
        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
