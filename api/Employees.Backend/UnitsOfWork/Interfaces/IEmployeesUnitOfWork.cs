
    using Employees.Shared.Entities;
    using Employees.Shared.Responses;
    namespace Employees.Backend.UnitsOfWork.Interfaces;
    public interface IEmployeesUnitOfWork : IGenericUnitOfWork<Employee>
    {
        Task<ActionResponse<IEnumerable<Employee>>> SearchByNameAsync(string term); 
    }

