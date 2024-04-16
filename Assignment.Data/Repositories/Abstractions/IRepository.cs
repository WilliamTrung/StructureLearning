using Assignment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repositories.Abstractions
{
    public interface IRepository<TEntity> where TEntity : IIdentity
    {
        Task<TEntity?> GetByIdAsync(string id);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<IEnumerable<TEntity>> InsertRangeAsync(IList<TEntity> entities);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> UpdateRange(IList<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Delete(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        void Delete(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
    }
}
