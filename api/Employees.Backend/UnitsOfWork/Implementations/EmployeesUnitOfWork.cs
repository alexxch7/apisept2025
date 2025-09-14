

    using Employees.Backend.Repositories.Interfaces;
    using Employees.Backend.UnitsOfWork.Interfaces;
    using Employees.Shared.Entities;
    using Employees.Shared.Responses;

    namespace Employees.Backend.UnitsOfWork.Implementations;
    public class EmployeesUnitOfWork : GenericUnitOfWork<Employee>, IEmployeesUnitOfWork
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesUnitOfWork(IGenericRepository<Employee> repository,
                                   IEmployeesRepository employeesRepository) : base(repository)
        {
            _employeesRepository = employeesRepository;
        }

        public Task<ActionResponse<IEnumerable<Employee>>> SearchByNameAsync(string term)
            => _employeesRepository.SearchByNameAsync(term);
    }

