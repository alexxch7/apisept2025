using Employees.Backend.Repositories.Interfaces;
using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.Responses;

namespace Employees.Backend.UnitsOfWork.Implementations;

    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        public GenericUnitOfWork(IGenericRepository<T> repository) => _repository = repository;

        public virtual Task<ActionResponse<IEnumerable<T>>> GetAsync() => _repository.GetAsync();
        public virtual Task<ActionResponse<T>> GetAsync(int id) => _repository.GetAsync(id);
        public virtual Task<ActionResponse<T>> AddAsync(T model) => _repository.AddAsync(model);
        public virtual Task<ActionResponse<T>> UpdateAsync(T model) => _repository.UpdateAsync(model);
        public virtual Task<ActionResponse<T>> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }


