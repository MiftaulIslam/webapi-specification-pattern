using SpecificationPattern.Models;
using SpecificationPattern.Specification.Base;

namespace SpecificationPattern.Specification.Developers;

public class DevWIthHighSalary:Specification<Developer>
{
    public DevWIthHighSalary(decimal minSalary):base(x => x.Income >= minSalary)
    {
        AddInclude(dev => dev.Address);
    }
}