using System.Linq.Expressions;
using Domain.Dtos;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly WriteDbContext _context;
	private readonly DbSet<T> _dbSet;

	public Repository(WriteDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _dbSet.FindAsync(id);
	}
	public  T? GetById(int id)
	{
		return _dbSet.Find(id);
	}

	public async Task<IEnumerable<T>> GetAllAsync(ParametersModels parametersModels)
	{
		return await _dbSet.ToListAsync();
	}
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
	public  IEnumerable<T> GetAll()
	{
		return  _dbSet.ToList();
	}
	public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
	{
		return await _dbSet.Where(predicate).ToListAsync();
	}

	public async Task AddAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
    }

	public async Task AddRangeAsync(IEnumerable<T> entities)
	{
		await _dbSet.AddRangeAsync(entities);
		await _context.SaveChangesAsync();

    }

    public void Update(T entity)
	{
		_dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Remove(T entity)
	{
		_dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<T> entities)
	{
		_dbSet.RemoveRange(entities);
        _context.SaveChanges();
    }

	public async Task UpdateAsync(T entity)
	{
		if (entity == null)
			throw new ArgumentNullException(nameof(entity));

		// Attach entity if not already tracked
		if (_context.Entry(entity).State == EntityState.Detached)
		{
			_dbSet.Attach(entity);
		}

		// Mark entity as modified
		_context.Entry(entity).State = EntityState.Modified;

		// Save changes asynchronously
		await _context.SaveChangesAsync();
	}


}
