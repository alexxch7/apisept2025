
    using Employees.Backend.UnitsOfWork.Interfaces;
    using Employees.Shared.Entities;
    using Microsoft.AspNetCore.Mvc;
    namespace Employees.Backend.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : GenericController<Employee>
    {
        private readonly IEmployeesUnitOfWork _employeesUoW;

        public EmployeesController(IGenericUnitOfWork<Employee> genericUoW,
                                   IEmployeesUnitOfWork employeesUoW) : base(genericUoW)
        {
            _employeesUoW = employeesUoW;
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] string term)
        {
            var resp = await _employeesUoW.SearchByNameAsync(term);
            return resp.WasSuccess ? Ok(resp.Result) : BadRequest(resp.Message);
        }
    }
