using Assignment.Data.Models.MongoModels;
using Assignment.Data.Repositories.Abstractions.Mongo;
using Assignment.MongoService.Abstractions;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.MongoService.Implements
{
    public class CategoryService : ServiceBase<Category, CategoryAddRequest, CategoryResponse, CategoryGetRequest>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository, ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }
    }
}
