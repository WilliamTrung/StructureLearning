using Assignment.Data.Contexts;
using Assignment.Data.Repositories.Abstractions;
using Assignment.Shared.Models;
using Assignment.Shared.Policies;
using Assignment.Shared.Provider.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repositories.Implements
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
         where TEntity : class, IIdentity
    {
        protected readonly DbSet<TEntity> _entities;
        protected readonly DbContext _context;
        private readonly IEnumerable<IDataPolicy> _policies;

        protected RepositoryBase(PostgreSqlContext dbContext, ICoreProvider coreProvider)
        {
            _context = dbContext;
            _entities = dbContext.Set<TEntity>();
            _policies = coreProvider.CreatePolicies<TEntity>() ?? new List<IDataPolicy>();
        }
        protected RepositoryBase(MongoContext dbContext, ICoreProvider coreProvider)
        {
            _context = dbContext;
            _entities = dbContext.Set<TEntity>();
            _policies = coreProvider.CreatePolicies<TEntity>() ?? new List<IDataPolicy>();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            if (applyGlobalFilter)
            {
                expression = ApplyFilter(expression);
            }

            return await (expression is null ? _entities.CountAsync() : _entities.CountAsync(expression));
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _entities.FindAsync(id);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            if (applyGlobalFilter)
            {
                expression = ApplyFilter(expression);
            }

            var entities = _entities.AsQueryable();

            if (expression is not null)
            {
                entities = entities.Where(expression);
            }

            return entities;
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            return await Find(expression, applyGlobalFilter).ToListAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            PrepareInsert(entity);
            await _entities.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> InsertRangeAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                PrepareInsert(entity);
            }

            await _entities.AddRangeAsync(entities);
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            PrepareUpdate(entity);
            _entities.Update(entity);
            return entity;
        }

        public IEnumerable<TEntity> UpdateRange(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                PrepareUpdate(entity);
            }

            _entities.UpdateRange(entities);
            return entities;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities.Any())
            {
                _entities.RemoveRange(entities);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            var entities = Find(expression, applyGlobalFilter);

            if (entities.Any())
            {
                _entities.RemoveRange(entities);
            }
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            if (applyGlobalFilter)
            {
                expression = ApplyFilter(expression);
            }
            return await (expression is null ? _entities.FirstOrDefaultAsync() : _entities.FirstOrDefaultAsync(expression));
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, bool applyGlobalFilter = false)
        {
            if (applyGlobalFilter)
            {
                expression = ApplyFilter(expression);
            }

            return await (expression is null ? _entities.AnyAsync() : _entities.AnyAsync(expression));
        }

        private void PrepareInsert(TEntity entity)
        {
            var insertPolicies = _policies.OfType<IInsertPolicy<TEntity>>();

            if (insertPolicies.Any())
            {
                foreach (var insertPolicy in insertPolicies)
                {
                    insertPolicy.PrepareInsert(entity);
                }
            }
        }

        private void PrepareUpdate(TEntity entity)
        {
            var updatePolicies = _policies.OfType<IUpdatePolicy<TEntity>>();

            if (updatePolicies.Any())
            {
                foreach (var updatePolicy in updatePolicies)
                {
                    updatePolicy.PrepareUpdate(entity);
                }
            }
        }

        private Expression<Func<TEntity, bool>>? ApplyFilter(Expression<Func<TEntity, bool>>? expression)
        {
            var filterPolicies = _policies.OfType<IFilterPolicy<TEntity>>();

            if (filterPolicies.Any())
            {
                foreach (var filterPolicy in filterPolicies)
                {
                    expression = filterPolicy.Predicate(expression);
                }
            }
            return expression;
        }
    }
}
