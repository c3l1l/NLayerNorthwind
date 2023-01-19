using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.Services
{
    public interface IService<T> where T : class
     {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);       
        Task<T> AddAsync(T entity);
        Task<T> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
