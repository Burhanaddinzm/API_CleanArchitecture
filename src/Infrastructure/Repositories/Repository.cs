using API.Application.Repositories;
using API.Domain.Common;
using API.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : BaseAuditableEntity
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        // Alternative :
        // await _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int? id)
    {
        if (await _context.Set<T>().FindAsync(id) is T entity)
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<T?> GetByIdAsync(int? id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? expression, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (expression != null)
        {
            return await query.FirstOrDefaultAsync(expression);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? where = null, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (where != null)
        {
            query = query.Where(where);
        }

        return await query.ToListAsync();
    }
}
