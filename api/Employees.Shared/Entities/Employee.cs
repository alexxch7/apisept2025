using System.ComponentModel.DataAnnotations;

namespace Employees.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [Display(Name = "First Name")]
    [MaxLength(30, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    [MaxLength(30, ErrorMessage = "The field {0} cannot have more than {1} characters")]
    [Required(ErrorMessage = "The field {0} is required")]
    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; } = true;


    [Display(Name = "Hire Date")]
    public DateTime? HireDate { get; set; }

    [Display(Name = "Salary")]
    [Range(1_000_000, double.MaxValue, ErrorMessage = "Minimum salary is {1}")]
    [Required(ErrorMessage = "The field {0} is required")]
    public decimal Salary { get; set; }
}
