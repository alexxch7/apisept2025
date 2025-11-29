using System.ComponentModel.DataAnnotations;

namespace Employees.Shared.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;

        [Display(Name = "Documento")]
        [MaxLength(20)]
        [Required]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [MaxLength(200)]
        [Required]
        public string Address { get; set; } = null!;

        [Display(Name = "Teléfono")]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
