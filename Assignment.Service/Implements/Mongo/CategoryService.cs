using Assignment.Data.Models.MongoModels;
using Assignment.Data.Repositories.Abstractions.Mongo;
using Assignment.Service.Abstractions.Mongo;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Implements.Mongo
{
    public class CategoryService : ServiceBase<Category, CategoryAddRequest, CategoryResponse, CategoryGetRequest>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository, ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }

        public async Task<bool> ValidateId(string? categoryId)
        {
            bool result = true;
            if (categoryId != null)
            {
                var find = await FindAsync(c => c.Id == categoryId);
                if (find == null || !find.Any())
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
