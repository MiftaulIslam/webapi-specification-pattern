using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Models;

namespace SpecificationPattern.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    DbSet<Developer> developers { get; set; }
    DbSet<Address> addresses { get; set; }
}