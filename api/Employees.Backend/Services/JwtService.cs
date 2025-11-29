using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Employees.Backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Employees.Backend.Services
{
    public interface IJwtService
    {
        Task<(string token, DateTime expires)> BuildTokenAsync(ApplicationUser user);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<(string token, DateTime expires)> BuildTokenAsync(ApplicationUser user)
        {

            var issuer = _config["Jwt:Issuer"]
                           ?? throw new InvalidOperationException("Missing config: Jwt:Issuer");
            var audience = _config["Jwt:Audience"]
                           ?? throw new InvalidOperationException("Missing config: Jwt:Audience");

            var rawKey = _config["Jwt:Key"]
                         ?? throw new InvalidOperationException("Missing config: Jwt:Key");
            var keyBytes = Encoding.UTF8.GetBytes(rawKey);


            if (keyBytes.Length < 32)
                throw new InvalidOperationException("Jwt:Key must be at least 32 bytes for HS256.");

            var minutesStr = _config["Jwt:ExpiresMinutes"];
            var minutes = int.TryParse(minutesStr, out var m) ? m : 240; // fallback si no está

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(minutes);


            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName ?? user.Email ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenStr, expires);
        }
    }
}
