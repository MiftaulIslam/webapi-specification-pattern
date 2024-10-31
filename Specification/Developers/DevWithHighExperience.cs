using SpecificationPattern.Models;
using SpecificationPattern.Specification.Base;
namespace SpecificationPattern.Specification.Developers;

public class DevWithHighExperience:Specification<Developer>
{
    public DevWithHighExperience(int minExp) : base(x => x.YearsOfExperience >= minExp)
    {
        AddInclude(x => x.Address);
    }
    
}