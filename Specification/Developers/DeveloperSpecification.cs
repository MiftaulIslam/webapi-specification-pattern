using System.Linq.Expressions;
using SpecificationPattern.Models;
using SpecificationPattern.Specification.Base;

namespace SpecificationPattern.Specification.Developers;

public class DeveloperSpecification:Specification<Developer>
{
    public DeveloperSpecification(
        int? minYearsOfExperience = null,
        decimal? minSalary = null,
        string? orderByField = null,
        bool orderByAscending = true,
        int? skip = null,
        int? take = null
    ) : base(
        d => 
            (!minYearsOfExperience.HasValue || d.YearsOfExperience >= minYearsOfExperience) && (!minSalary.HasValue || d.Income >= minSalary)
        )
    {
        if (!String.IsNullOrEmpty(orderByField))
        {
            Func<Expression<Func<Developer, object>>> orderExpression = (orderByField.ToLower() switch
            {
                "yearsofexperience" => () => d => d.YearsOfExperience,
                "salary" => () => d => d.Income,
                _ => null,
            })!;
            if (orderExpression != null)
            {
                if (!orderByAscending)
                {
                    ApplyOrderByDescending(orderExpression());
                }
                else
                {
                    ApplyOrderBy(orderExpression());
                }
            }
        }

        if (take.HasValue)
        {
            ApplyPaging(skip, take);
        }
    }
}