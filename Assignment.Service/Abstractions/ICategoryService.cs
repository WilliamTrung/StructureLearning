using Assignment.Data.Models;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Abstractions
{
    public interface ICategoryService : IService<Category, CategoryAddRequest, CategoryResponse, CategoryGetRequest>
    {
    }
}
