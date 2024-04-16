using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Category;
using Assignment.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Abstractions.Mongo
{
    public interface ICategoryBusiness : IBusiness
    {
        Task<CategoryResponse> Create(CategoryAddRequest request);
        Task<CategoryResponse> Update(CategoryUpdateRequest request);
        Task<IEnumerable<CategoryResponse>> GetAll();
        Task<PagedResult<CategoryResponse>> GetPaged(CategoryGetRequest request);
        Task Delete(string id);
    }
}
