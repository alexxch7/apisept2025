using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Responses;

namespace Employees.Backend.UnitsOfWork.Implementations
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual Task<ActionResponse<IEnumerable<T>>> GetAsync() => _repository.GetAsync();
        public virtual Task<ActionResponse<T>> GetAsync(int id) => _repository.GetAsync(id);
        public virtual Task<ActionResponse<T>> AddAsync(T model) => _repository.AddAsync(model);
        public virtual Task<ActionResponse<T>> UpdateAsync(T model) => _repository.UpdateAsync(model);
        public virtual Task<ActionResponse<T>> DeleteAsync(int id) => _repository.DeleteAsync(id);

        public virtual Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
            => _repository.GetAsync(pagination);

        public virtual Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
            => _repository.GetTotalRecordsAsync(pagination);
    }
}
