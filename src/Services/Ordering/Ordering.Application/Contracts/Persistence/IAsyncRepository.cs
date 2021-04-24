using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
		// For Fetching Data
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
										Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
										string includeString = null,
										bool disableTracking = true);
		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									   List<Expression<Func<T, object>>> includes = null,
									   bool disableTracking = true);
		Task<T> GetByIdAsync(int id);

		// For Manipulating Data
		Task<T> AddAsync(T entity);
		Task<bool> UpdateAsync(T entity);
		Task<bool> DeleteAsync(T entity);

		// For Manipulating Batch Data
		Task<bool> UpdateAsync(IList<T> entities);
		Task<bool> AddAsync(IList<T> entities);
		Task<bool> DeleteAsync(IList<T> entities);
	}
}
