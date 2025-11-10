using Employees.Backend.Data;
using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        private readonly DataContext _context;

        public CitiesController(IGenericUnitOfWork<City> uow, DataContext context) : base(uow)
        {
            _context = context;
        }

        // Ciudades por Estado
        [HttpGet("byState/{stateId:int}")]
        public async Task<IActionResult> GetByStateAsync(int stateId)
        {
            var list = await _context.Cities
                .Where(c => c.StateId == stateId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(list);
        }
    }
}
