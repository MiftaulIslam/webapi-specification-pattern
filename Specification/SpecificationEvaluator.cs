using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Models.Base;
using SpecificationPattern.Specification.Base;

namespace SpecificationPattern.Specification;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        foreach (Expression<Func<T, object>> include in specification.Includes)
        {
            query = query.Include(include);
        }
        foreach (string include in specification.IncludeStrings)
        {
            query = query.Include(include);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        // Apply pagination
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult>specification)
    {
        IQueryable<T> query = GetQuery(inputQuery, (ISpecification<T>)specification);
        return specification.Select !=null? query.Select(specification.Select):query.Cast<TResult>();
    }
}