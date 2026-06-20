using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IRepository<T> where T : class
{
	Task<T?> GetByIdAsync(int id);
	public T? GetById(int id);
	public IEnumerable<T> GetAll();

	Task<IEnumerable<T>> GetAllAsync(ParametersModels  parametersModels);
	Task<IEnumerable<T>> GetAllAsync();
	Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

	Task AddAsync(T entity);
	Task AddRangeAsync(IEnumerable<T> entities);

	void Update(T entity);
	Task UpdateAsync(T entity);

	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
}
