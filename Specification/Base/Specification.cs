using System.Linq.Expressions;

namespace SpecificationPattern.Specification.Base;

public class Specification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
    where T : class
{
    public Expression<Func<T, bool>> Criteria { get; } = criteria;
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public List<string> IncludeStrings { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get;private set; }
    public bool IsPagingEnabled { get; private set; } = false;
    public int Skip { get; private set; } = 0;
    public int Take { get; private set; } = 0;
    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    public void ApplyPaging(int? skip, int? take)
    {
        IsPagingEnabled = true;
        Skip = skip ?? 0;
        Take = take ?? 10;
    }
}

public class Specification<T, TResult>(Expression<Func<T, bool>> criteria)
    : Specification<T>(criteria), ISpecification<T, TResult> where T : class
{
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }    
}