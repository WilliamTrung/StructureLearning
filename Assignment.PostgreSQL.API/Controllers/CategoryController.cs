using Assignment.Business.Abstractions;
using Assignment.Shared.Constants;
using Assignment.Shared.Requests.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(CategoryAddRequest request)
        {
            var res = await _business.Create(request);
            return CreateOkForResponse(res);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateRequest request)
        {
            var res = await _business.Update(request);
            return CreateOkForResponse(res);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string categoryId)
        {
            await _business.Delete(categoryId);
            return NoContent();
        }
    }
}
