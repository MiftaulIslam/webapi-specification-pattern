using SpecificationPattern.Models.Base;
using SpecificationPattern.Specification.Base;

namespace SpecificationPattern.Repository.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification);
    Task<T> GetById(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
    
}