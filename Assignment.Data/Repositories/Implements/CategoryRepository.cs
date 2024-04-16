using Assignment.Data.Contexts;
using Assignment.Data.Models;
using Assignment.Data.Repositories.Abstractions;
using Assignment.Shared.Provider.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repositories.Implements
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
