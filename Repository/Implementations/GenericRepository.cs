using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Data;
using SpecificationPattern.Models.Base;
using SpecificationPattern.Repository.Interfaces;
using SpecificationPattern.Specification;
using SpecificationPattern.Specification.Base;

namespace SpecificationPattern.Repository.Implementations;

public class GenericRepository<T>:IGenericRepository<T> where T:BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification)
    {
        var query = SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specification);
        return await query.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if(entity == null) throw new KeyNotFoundException();
        return entity;
    }

    public async Task AddAsync(T entity)
    {
         await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}