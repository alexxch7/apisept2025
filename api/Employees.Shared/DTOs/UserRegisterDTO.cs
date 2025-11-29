namespace Employees.Shared.DTOs
{
    public class UserRegisterDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? Photo { get; set; }
    }
}
