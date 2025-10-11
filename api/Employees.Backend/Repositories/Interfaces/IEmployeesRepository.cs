using Employees.Shared.Entities;
using Employees.Shared.Responses;

namespace Employees.Backend.Repositories.Interfaces
{
    public interface IEmployeesRepository : IGenericRepository<Employee>
    {
        Task<ActionResponse<IEnumerable<Employee>>> SearchByNameAsync(string term);
    }
}
