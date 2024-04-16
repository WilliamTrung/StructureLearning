using Assignment.Data.Contexts;
using Assignment.Data.Exceptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly DbContext _dbContext;
        public UnitOfWorkService(PostgreSqlContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UnitOfWorkService(MongoContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync() => await SaveChangesAsync(_dbContext);

        private async Task SaveChangesAsync<T>(T dbContext) where T : DbContext
        {
            try
            {
                var test = await dbContext.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                foreach (EntityEntry entityEntry in exception.Entries)
                {
                    await entityEntry.ReloadAsync().ConfigureAwait(false);
                }

                IEnumerable<object> entities = exception.Entries.Select(entry => entry.Entity);
                throw new DataConflictException(entities);
            }
        }
    }
}
