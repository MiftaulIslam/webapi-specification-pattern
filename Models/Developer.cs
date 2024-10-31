using System.ComponentModel.DataAnnotations;
using SpecificationPattern.Models.Base;

namespace SpecificationPattern.Models;

public class Developer:BaseEntity
{
    public string Name { get; set; }= string.Empty;
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Developer's years of experience is required")]
    public int YearsOfExperience { get; set; }
    public decimal Income { get; set; }
    public Address? Address { get; set; }
}