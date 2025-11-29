using System.Security.Claims;
using Employees.Backend.Entities;
using Employees.Backend.Services;
using Employees.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Photo = model.Photo
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }

            return Ok("User created");
        }


        [HttpPost("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Incorrect username or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest("Incorrect username or password.");
            }

            var (token, expires) = await _jwtService.BuildTokenAsync(user);

            return Ok(new TokenDTO
            {
                Token = token,
                Expiration = expires
            });
        }


        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetProfile()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            var dto = new UserDTO
            {
                Email = user.Email!,
                Photo = user.Photo
            };

            return Ok(dto);
        }


        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserDTO model)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            user.Email = model.Email;
            user.UserName = model.Email;
            user.Photo = model.Photo;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }

            return NoContent();
        }


        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                return BadRequest("The new password confirmation does not match.");
            }

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _userManager.ChangePasswordAsync(
                user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }

            return NoContent();
        }

        private async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            if (email == null)
            {
                return null;
            }

            return await _userManager.FindByEmailAsync(email);
        }
    }
}
