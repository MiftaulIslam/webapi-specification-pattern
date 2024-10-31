using System.Linq.Expressions;

namespace SpecificationPattern.Specification.Base;

public interface ISpecification<T> where T:class
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>?OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool IsPagingEnabled { get; }
    int Skip { get; }
    int Take { get; }
    void AddInclude(Expression<Func<T, object>> includeExpression);
    void AddInclude(string includeString);
    void ApplyOrderBy(Expression<Func<T, object>> orderByExpression);
    void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression);
    void ApplyPaging(int? skip, int? take);
    
}

public interface ISpecification<T, TResult> : ISpecification<T> where T:class
{
    Expression<Func<T, TResult>>? Select { get; }
}