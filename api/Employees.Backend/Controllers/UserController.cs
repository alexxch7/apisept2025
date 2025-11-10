using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Employees.Backend.Entities;
using Employees.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDTO>> Login(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized();

            var ok = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!ok) return Unauthorized();

            var token = BuildToken(user.Email!);
            return Ok(token);
        }

        private TokenDTO BuildToken(string email)
        {
            var jwt = _configuration.GetSection("Jwt");
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiresInMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new TokenDTO { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = expiration };
        }
    }
}
