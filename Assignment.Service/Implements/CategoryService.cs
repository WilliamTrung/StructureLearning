using Assignment.Data.Models;
using Assignment.Data.Repositories.Abstractions;
using Assignment.Service.Abstractions;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Implements
{
    public class CategoryService : ServiceBase<Category, CategoryAddRequest, CategoryResponse, CategoryGetRequest>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository, ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }
    }
}
