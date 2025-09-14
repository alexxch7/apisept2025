
    using Employees.Backend.UnitsOfWork.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    namespace Employees.Backend.Controllers;
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericUnitOfWork<T> _unitOfWork;
        public GenericController(IGenericUnitOfWork<T> unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var resp = await _unitOfWork.GetAsync();
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var resp = await _unitOfWork.GetAsync(id);
            return resp.WasSuccess ? Ok(resp.Result) : NotFound(resp.Message);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody] T model)
        {
            var resp = await _unitOfWork.AddAsync(model);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync([FromBody] T model)
        {
            var resp = await _unitOfWork.UpdateAsync(model);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var resp = await _unitOfWork.DeleteAsync(id);
            return resp.WasSuccess ? NoContent() : BadRequest(resp.Message);
        }
    }

