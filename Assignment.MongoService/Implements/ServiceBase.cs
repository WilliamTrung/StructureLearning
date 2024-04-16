using Assignment.Data.Repositories.Abstractions;
using Assignment.Shared.Models;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests;
using Assignment.Shared.Responses;
using AutoMapper;
using Assignment.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Assignment.Shared.Constants;
using Assignment.MongoService.Abstractions;

namespace Assignment.MongoService.Implements
{
    public abstract class ServiceBase<TEntity> : IService<TEntity>
           where TEntity : BaseMongoEntity, IIdentity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly ICoreProvider _coreProvider;

        protected ServiceBase(IRepository<TEntity> repository,
            ICoreProvider coreProvider)
        {
            _repository = repository;
            _mapper = coreProvider.Mapper;
            _coreProvider = coreProvider;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            return await _repository.CountAsync(expression, applyGlobalFilter);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public virtual void SoftDelete(TEntity entity)
        {
            entity.IsActive = false;
            _repository.Update(entity);
        }

        public virtual void SoftDeleteById(string id)
        {
            var item = this.Find(x => x.Id == id).FirstOrDefault();
            if (item != null)
            {
                SoftDelete(item);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            _repository.Delete(expression, applyGlobalFilter);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _repository.DeleteRange(entities);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            return _repository.Find(expression, applyGlobalFilter);
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            return await _repository.FindAsync(expression, applyGlobalFilter);
        }

        public async Task<List<TResponse>> FindAsync<TResponse>(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            var entities = await FindAsync(expression, applyGlobalFilter);
            return _mapper.Map<List<TResponse>>(entities);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            return await _repository.FirstOrDefaultAsync(expression, applyGlobalFilter);
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var test = await _repository.InsertAsync(entity);
            return test;
        }

        public async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            return await _repository.InsertRangeAsync(entities.ToList());
        }

        public TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateRange(entities.ToList());
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            return await _repository.AnyAsync(expression, applyGlobalFilter);
        }
    }

    public abstract class ServiceBase<TEntity, TResponse> : ServiceBase<TEntity>, IService<TEntity, TResponse>
        where TEntity : BaseMongoEntity, IIdentity
        where TResponse : class
    {
        protected ServiceBase(IRepository<TEntity> repository,
            ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }

        public async Task<List<TResponse>> FindResponseAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            var entities = await FindAsync(expression, applyGlobalFilter);
            return _mapper.Map<List<TResponse>>(entities);
        }

        public async Task<TResponse?> FirstOrDefaultResponseAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            var entity = await FirstOrDefaultAsync(expression, applyGlobalFilter);
            return entity is null ? null : _mapper.Map<TResponse>(entity);
        }

        public async Task<TResponse?> GetResponseByIdAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            return entity is null ? null : _mapper.Map<TResponse>(entity);
        }
    }

    public abstract class ServiceBase<TEntity, TRequest, TResponse> : ServiceBase<TEntity, TResponse>, IService<TEntity, TRequest, TResponse>
        where TEntity : BaseMongoEntity, IIdentity
        where TRequest : BaseRequest
        where TResponse : class
    {
        protected ServiceBase(IRepository<TEntity> repository,
            ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }

        public Task<TEntity> InsertAsync(TRequest request, Action<TEntity>? prepareInsert = null)
        {
            var entity = MapEntityForInsert(request);
            prepareInsert?.Invoke(entity);
            return InsertAsync(entity);
        }

        public Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TRequest> requests, Action<TEntity>? prepareInsert = null)
        {
            var entities = requests.Select(x =>
            {
                var entity = MapEntityForInsert(x);
                prepareInsert?.Invoke(entity);
                return entity;
            });
            return InsertRangeAsync(entities);
        }

        public async Task<TEntity> UpdateAsync(TRequest request)
        {
            var entity = await PrepareUpdateAsync(request);

            if (entity is not null)
            {
                entity = Update(entity);
                return entity;
            }
            else
            {
                throw new KeyNotFoundException("entity not found for id: " + request.Id);
            }
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TRequest> requests)
        {
            IEnumerable<TEntity> entities = new List<TEntity>();

            foreach (var request in requests)
            {
                var entity = await PrepareUpdateAsync(request);

                if (entity is not null)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    (entities as List<TEntity>).Add(entity);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }

            if (entities.Any())
            {
                entities = UpdateRange(entities);
            }

            return entities;
        }

        protected virtual TEntity MapEntityForInsert(TRequest request)
        {
            return _mapper.Map<TEntity>(request);
        }

        protected virtual void MapEntityForUpdate(TRequest fromRequest, TEntity toEntity)
        {
            _mapper.Map(fromRequest, toEntity);
        }

        private async Task<TEntity?> PrepareUpdateAsync(TRequest request)
        {
            TEntity? entity = null;

            if (!string.IsNullOrWhiteSpace(request.Id))
            {
                entity = await GetByIdAsync(request.Id);

                if (entity is not null)
                {
                    MapEntityForUpdate(request, entity);
                }
            }

            return entity;
        }
    }

    public abstract class GetAllServiceBase<TEntity, TGetAllRequest, TResponse> : ServiceBase<TEntity, TResponse>, IGetAllService<TEntity, TGetAllRequest, TResponse>
        where TEntity : BaseMongoEntity, IIdentity
        where TGetAllRequest : BaseGetAllRequest
        where TResponse : class
    {
        protected GetAllServiceBase(IRepository<TEntity> repository,
            ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }

        public async Task<IEnumerable<TResponse>> GetAllAsync(TGetAllRequest getAllRequest, bool applyGlobalFilter = false)
        {
            var entities = await GetAll(getAllRequest, applyGlobalFilter, Find, GetFilterExpressionForGetAll, ApplyOrderByForGetAll, ApplyIncludeForGetAll).ToListAsync();
            return _mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public async Task<PagedResult<TResponse>> GetAllPagingAsync(TGetAllRequest getAllRequest, bool applyGlobalFilter = false)
        {
            return await GetAllPagingAsync(_mapper, getAllRequest, applyGlobalFilter, Find, GetFilterExpressionForGetAll, ApplyOrderByForGetAll, ApplyIncludeForGetAll);
        }

        protected virtual IQueryable<TEntity> ApplyIncludeForGetAll(IQueryable<TEntity> entities) => entities;

        protected virtual IQueryable<TEntity> ApplyOrderByForGetAll(IQueryable<TEntity> entities, TGetAllRequest getAllRequest)
            => BaseApplyOrderByForGetAll(entities, getAllRequest);

        protected virtual Expression<Func<TEntity, bool>> GetFilterExpressionForGetAll(TGetAllRequest request)
            => BaseGetFilterExpressionForGetAll(request);

        internal static IQueryable<TEntity> BaseApplyOrderByForGetAll(IQueryable<TEntity> entities, TGetAllRequest getAllRequest)
        {
            if (!string.IsNullOrEmpty(getAllRequest.SortField))
            {
                entities = entities.OrderByDynamic(getAllRequest.SortField, getAllRequest.Asc ?? true);
            }

            return entities;
        }

        internal static Expression<Func<TEntity, bool>> BaseGetFilterExpressionForGetAll(TGetAllRequest request)
        {
            if (string.IsNullOrEmpty(request.Filter))
            {
                // return PredicateBuilder.New<TEntity>(entity => true);
                Expression<Func<TEntity, bool>> e = entity => true;
                return e;
            }

            return entity => EF.Functions.Like(entity.Id, $"%{request.Filter}%");
        }

        internal async static Task<PagedResult<TResponse>> GetAllPagingAsync(IMapper mapper, TGetAllRequest getAllRequest, bool applyGlobalFilter = false, Func<Expression<Func<TEntity, bool>>, bool, IQueryable<TEntity>>? queryFunc = null,
            Func<TGetAllRequest, Expression<Func<TEntity, bool>>>? getFilterExpressionFunc = null, Func<IQueryable<TEntity>, TGetAllRequest, IQueryable<TEntity>>? applyOrderByFunc = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? applyIncludeFunc = null)
        {
            var query = GetAll(getAllRequest, applyGlobalFilter, queryFunc, getFilterExpressionFunc, applyOrderByFunc);
            var count = await query.CountAsync();
            var pageSize = getAllRequest.PageSize ?? Common.PageSizeMin;
            pageSize = Math.Min(pageSize, Common.PageSizeMax);
            pageSize = Math.Max(pageSize, Common.PageSizeMin);
            var pageIndex = Math.Max(getAllRequest.PageIndex, 0);
            query = query.Skip(pageSize * pageIndex).Take(pageSize);
            var entities = await applyIncludeFunc.Invoke(query).ToListAsync();
            return new PagedResult<TResponse>()
            {
                Results = mapper.Map<IList<TResponse>>(entities),
                CurrentPage = pageIndex,
                RowCount = count,
                PageSize = pageSize
            };
        }

        internal static IQueryable<TEntity> GetAll(TGetAllRequest getAllRequest, bool applyGlobalFilter = false, Func<Expression<Func<TEntity, bool>>, bool, IQueryable<TEntity>>? queryFunc = null,
            Func<TGetAllRequest, Expression<Func<TEntity, bool>>>? getFilterExpressionFunc = null, Func<IQueryable<TEntity>, TGetAllRequest, IQueryable<TEntity>>? applyOrderByFunc = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? applyIncludeFunc = null)
        {
            var query = queryFunc.Invoke(null, applyGlobalFilter)
                .Where(getFilterExpressionFunc.Invoke(getAllRequest));
            query = applyOrderByFunc.Invoke(query, getAllRequest);

            if (applyIncludeFunc is not null)
            {
                query = applyIncludeFunc.Invoke(query);
            }

            return query;
        }
    }

    public abstract class ServiceBase<TEntity, TRequest, TResponse, TGetAllRequest> : ServiceBase<TEntity, TRequest, TResponse>, IService<TEntity, TRequest, TResponse, TGetAllRequest>
        where TEntity : BaseMongoEntity, IIdentity
        where TGetAllRequest : BaseGetAllRequest
        where TRequest : BaseRequest
        where TResponse : class
    {
        protected ServiceBase(IRepository<TEntity> repository,
            ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }

        public async Task<IEnumerable<TResponse>> GetAllAsync(TGetAllRequest getAllRequest, bool applyGlobalFilter = false)
        {
            var entities = await GetAllServiceBase<TEntity, TGetAllRequest, TResponse>.GetAll(getAllRequest, applyGlobalFilter, Find, GetFilterExpressionForGetAll, ApplyOrderByForGetAll, ApplyIncludeForGetAll).ToListAsync();
            return _mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public async Task<PagedResult<TResponse>> GetAllPagingAsync(TGetAllRequest getAllRequest, bool applyGlobalFilter = false)
        {
            return await GetAllServiceBase<TEntity, TGetAllRequest, TResponse>.GetAllPagingAsync(_mapper, getAllRequest, applyGlobalFilter, Find, GetFilterExpressionForGetAll, ApplyOrderByForGetAll, ApplyIncludeForGetAll);
        }

        protected virtual IQueryable<TEntity> ApplyIncludeForGetAll(IQueryable<TEntity> entities) => entities;

        protected virtual IQueryable<TEntity> ApplyOrderByForGetAll(IQueryable<TEntity> entities, TGetAllRequest getAllRequest)
            => GetAllServiceBase<TEntity, TGetAllRequest, TResponse>.BaseApplyOrderByForGetAll(entities, getAllRequest);

        protected virtual Expression<Func<TEntity, bool>> GetFilterExpressionForGetAll(TGetAllRequest request)
            => GetAllServiceBase<TEntity, TGetAllRequest, TResponse>.BaseGetFilterExpressionForGetAll(request);
    }
}
