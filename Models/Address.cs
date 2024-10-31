using System.ComponentModel.DataAnnotations;
using SpecificationPattern.Models.Base;

namespace SpecificationPattern.Models;

public class Address:BaseEntity
{
    public string City { get; set; } = string.Empty;
    public string Region { get; set; }= string.Empty;
}