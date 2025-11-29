using Employees.Backend.Entities;
using Employees.Backend.Services;
using Employees.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwt;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized();

            var valid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!valid)
                return Unauthorized();

            var (token, expires) = await _jwt.BuildTokenAsync(user);
            return Ok(new TokenDTO { Token = token, Expiration = expires });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(string.Join("; ", result.Errors.Select(e => e.Description)));


            var (token, expires) = await _jwt.BuildTokenAsync(user);
            return Ok(new TokenDTO { Token = token, Expiration = expires });
        }
    }
}
