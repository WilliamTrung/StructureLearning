using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses.Product;
using Assignment.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Abstractions.Mongo
{
    public interface IProductBusiness : IBusiness
    {
        Task<ProductResponse> Create(ProductAddRequest request);
        Task<ProductResponse> Update(ProductUpdateRequest request);
        Task<ProductResponse> UpdateCategory(ProductUpdateCategoryRequest request);
        Task<IEnumerable<ProductResponse>> GetAll();
        Task<PagedResult<ProductResponse>> GetPaged(ProductGetRequest request);
        Task Delete(string id);
    }
}
