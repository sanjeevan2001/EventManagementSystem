using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventManagement.Application.Abstraction.Persistences.IRepositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Guid id);
        
        Task AddAsync(T entity);       
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task SaveChangesAsync();
    }
}
