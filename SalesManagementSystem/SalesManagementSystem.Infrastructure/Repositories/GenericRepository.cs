namespace SalesManagementSystem.Infrastructure.Repositories;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly SalesManagementContext _salesManagementContext;

    public GenericRepository(SalesManagementContext salesManagementContext)
    {
        _salesManagementContext = salesManagementContext;
    }

    public async Task Add(T entity)
    {
        await _salesManagementContext.Set<T>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _salesManagementContext.Set<T>().AddRangeAsync(entities);
    }

    public void Delete(T entity)
    {
        _salesManagementContext.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAll( Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _salesManagementContext.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetById(Expression<Func<T, bool>> predicate, string includeProperties = "")
    {
        IQueryable<T> query = _salesManagementContext.Set<T>();

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.FirstOrDefaultAsync(predicate);
    }

    public void Update(T entity)
    {
        _salesManagementContext.Set<T>().Update(entity);
    }
    
}

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");
    Task<T> GetById(Expression<Func<T, bool>> predicate, string includeProperties = "");
    Task Add(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Delete(T entity);
    void Update(T entity);
}

