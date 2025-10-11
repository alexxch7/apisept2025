using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.DTOs;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : GenericController<Employee>
    {
        private readonly IEmployeesUnitOfWork _uow;

        public EmployeesController(IEmployeesUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            var resp = await _uow.SearchByNameAsync(term);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginationDTO dto)
        {
            var resp = await _uow.GetAsync(dto);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }

        [HttpGet("totalRecords")]
        public async Task<IActionResult> GetTotalRecords([FromQuery] PaginationDTO dto)
        {
            var resp = await _uow.GetTotalRecordsAsync(dto);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }
    }
}
