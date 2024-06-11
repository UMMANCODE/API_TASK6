using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TASK3_DataAccess.Repositories.Interfaces {
  public interface IRepository<T> where T : class {
    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 1, params string[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, params string[] includes);
    Task AddAsync(T entity);
    Task SaveAsync();
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, params string[] includes);
    Task DeleteAsync(T entity);
  }
}
