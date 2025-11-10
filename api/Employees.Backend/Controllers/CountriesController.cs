using Employees.Backend.Data;
using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Countries>
    {
        private readonly DataContext _context;

        public CountriesController(IGenericUnitOfWork<Countries> uow, DataContext context) : base(uow)
        {
            _context = context;
        }

        [HttpGet("full")]
        public async Task<IActionResult> GetFullAsync()
        {
            var list = await _context.Countries
                .Include(c => c.States)
                    .ThenInclude(s => s.Cities)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(list);
        }
    }
}
