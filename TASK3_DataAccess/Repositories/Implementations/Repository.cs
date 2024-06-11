using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TASK3_DataAccess.Repositories.Interfaces;

namespace TASK3_DataAccess.Repositories.Implementations {
  public class Repository<T> : IRepository<T> where T : class, new() {
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) {
      _context = context;
    }

    public async Task AddAsync(T entity) {
      await _context.Set<T>().AddAsync(entity);
    }

    public async Task DeleteAsync(T entity) {
      await Task.Run(() => _context.Set<T>().Remove(entity));
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, params string[] includes) {
      var query = _context.Set<T>().AsQueryable();

      foreach (var include in includes) {
        query = query.Include(include);
      }

      return await query.AnyAsync(predicate);
    }

    public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 1, params string[] includes) {
      var query = _context.Set<T>().AsQueryable();

      foreach (var include in includes) {
        query = query.Include(include);
      }

      query = query.Where(predicate)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);

      return await Task.Run(() => query);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params string[] includes) {
      var query = _context.Set<T>().AsQueryable();

      foreach (var include in includes) {
        query = query.Include(include);
      }

      return await query.FirstOrDefaultAsync(predicate) ?? new T();
    }

    public async Task SaveAsync() {
      await _context.SaveChangesAsync();
    }
  }
}
