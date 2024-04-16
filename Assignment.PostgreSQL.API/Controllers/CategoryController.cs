using Assignment.Business.Abstractions;
using Assignment.Shared.Constants;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Product;
using Assignment.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Assignment.Shared.Responses.Category;

namespace Assignment.PostgreSQL.API.Controllers
{
    [Route("api/category")]
    [Authorize(Roles = RoleName.Admin)]
    [ApiController]
    public class CategoryController : ControllerBase<ICategoryBusiness>
    {
        public CategoryController(ICategoryBusiness business) : base(business)
        {
        }
        [HttpGet]
        [ProducesResponseType(typeof(ActionResponse<IEnumerable<CategoryResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var res = await _business.GetAll();
            return CreateOkForResponse(res);
        }
        //[HttpGet]
        //[Route("paging")]
        //public async Task<IActionResult> GetPaged(CategoryGetRequest request)
        //{
        //    var categories = await _business.GetPaged(request);
        //    return CreateOkForResponse(categories);
        //}
        [HttpPost]
        [ProducesResponseType(typeof(ActionResponse<CategoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CategoryAddRequest request)
        {
            var res = await _business.Create(request);
            return CreateOkForResponse(res);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ActionResponse<CategoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(CategoryUpdateRequest request)
        {
            var res = await _business.Update(request);
            return CreateOkForResponse(res);
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ActionResponse), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string categoryId)
        {
            await _business.Delete(categoryId);
            return NoContent();
        }
    }
}
