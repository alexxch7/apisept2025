using System.ComponentModel.DataAnnotations;

namespace Employees.Shared.DTOs
{
    public class LoginDTO
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(20, MinimumLength = 6)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
