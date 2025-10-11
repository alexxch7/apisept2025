using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericUnitOfWork<T> _uow;

        protected GenericController(IGenericUnitOfWork<T> uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resp = await _uow.GetAsync();
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var resp = await _uow.GetAsync(id);
            return resp.WasSuccess ? Ok(resp.Result) : NotFound(resp.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T model)
        {
            var resp = await _uow.AddAsync(model);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] T model)
        {
            var resp = await _uow.UpdateAsync(model);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _uow.DeleteAsync(id);
            return resp.WasSuccess ? Ok() : BadRequest(resp.Message);
        }
    }
}
