using Assignment.Shared.Models;
using Assignment.Shared.Requests;
using Assignment.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Abstractions
{
    public interface IService<TEntity> where TEntity : IIdentity
    {
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        Task<TEntity?> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> InsertAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Delete(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        void SoftDelete(TEntity entity);
        void SoftDeleteById(string id);
        void Delete(TEntity entity);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
    }

    public interface IService<TEntity, TResponse> : IService<TEntity>
        where TEntity : IIdentity
    {
        Task<TResponse?> GetResponseByIdAsync(string id);
        Task<TResponse?> FirstOrDefaultResponseAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
        Task<List<TResponse>> FindResponseAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false);
    }

    public interface IService<TEntity, TRequest, TResponse> : IService<TEntity, TResponse>
        where TEntity : IIdentity
        where TRequest : BaseRequest
    {
        Task<TEntity> InsertAsync(TRequest request, Action<TEntity>? prepareInsert = null);
        Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TRequest> requests, Action<TEntity>? prepareInsert = null);
        Task<TEntity> UpdateAsync(TRequest request);
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TRequest> requests);
    }

    public interface IGetAllService<TEntity, TGetAllRequest, TResponse> : IService<TEntity, TResponse>
        where TEntity : IIdentity
        where TGetAllRequest : BaseGetAllRequest
        where TResponse : class
    {
        Task<IEnumerable<TResponse>> GetAllAsync(TGetAllRequest getAllRequest, bool applyGlobalFilter = false);
        Task<PagedResult<TResponse>> GetAllPagingAsync(TGetAllRequest getAllRequest, bool applyGlobalFilter = false);
    }

    public interface IService<TEntity, TRequest, TResponse, TGetAllRequest> : IService<TEntity, TRequest, TResponse>,
        IGetAllService<TEntity, TGetAllRequest, TResponse>
        where TEntity : IIdentity
        where TGetAllRequest : BaseGetAllRequest
        where TRequest : BaseRequest
        where TResponse : class
    {
    }
}
