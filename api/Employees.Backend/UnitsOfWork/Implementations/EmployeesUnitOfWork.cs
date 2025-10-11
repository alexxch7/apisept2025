using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Entities;
using Employees.Shared.Responses;

namespace Employees.Backend.UnitsOfWork.Implementations
{
    public class EmployeesUnitOfWork : GenericUnitOfWork<Employee>, IEmployeesUnitOfWork
    {
        private readonly IEmployeesRepository _repo;

        public EmployeesUnitOfWork(IGenericRepository<Employee> genericRepo, IEmployeesRepository repo)
            : base(genericRepo)
        {
            _repo = repo;
        }

        public Task<ActionResponse<IEnumerable<Employee>>> SearchByNameAsync(string term)
            => _repo.SearchByNameAsync(term);

        public Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO dto)
            => _repo.GetAsync(dto);

        public Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO dto)
            => _repo.GetTotalRecordsAsync(dto);
    }
}
