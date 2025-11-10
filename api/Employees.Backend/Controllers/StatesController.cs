using Employees.Backend.Data;
using Employees.Backend.UnitsOfWork.Interfaces;
using Employees.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : GenericController<State>
    {
        private readonly DataContext _context;

        public StatesController(IGenericUnitOfWork<State> uow, DataContext context) : base(uow)
        {
            _context = context;
        }

        [HttpGet("byCountry/{countryId:int}")]
        public async Task<IActionResult> GetByCountryAsync(int countryId)
        {
            var list = await _context.States
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(list);
        }
    }
}
