using Assignment.Data.Contexts;
using Assignment.Data.Models.MongoModels;
using Assignment.Data.Repositories.Abstractions.Mongo;
using Assignment.Shared.Provider.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repositories.Implements.Mongo
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(PostgreSqlContext dbContext, ICoreProvider coreProvider) : base(dbContext, coreProvider)
        {
        }
        public CategoryRepository(MongoContext dbContext, ICoreProvider coreProvider) : base(dbContext, coreProvider)
        {
        }
    }
}
