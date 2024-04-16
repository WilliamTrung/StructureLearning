using Assignment.Business.Abstractions.Mongo;
using Assignment.Shared.Constants;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses;
using Assignment.Shared.Responses.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.Contracts;
using System.Net;

namespace Assignment.Mongo.API.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase<IProductBusiness>
    {
        public ProductController(IProductBusiness business) : base(business)
        {
        }
        [HttpGet]
        [ProducesResponseType(typeof(ActionResponse<IEnumerable<ProductResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var response = await _business.GetAll();
            return CreateOkForResponse(response);
        }
        //[HttpGet]
        //[Route("paging")]
        //public async Task<IActionResult> GetPaged(ProductGetRequest request)
        //{
        //    var response = await _business.GetPaged(request);
        //    return CreateOkForResponse(response);
        //}
        [HttpPost]
        [ProducesResponseType(typeof(ActionResponse<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ProductAddRequest request)
        {
            var product = await _business.Create(request);
            return CreateOkForResponse(product);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ActionResponse<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            var product = await _business.Update(request);
            return CreateOkForResponse(product);
        }
        //[HttpPut]
        //[Route("/update-category")]
        //public async Task<IActionResult> UpdateCategroy(ProductUpdateCategoryRequest request)
        //{
        //    var product = await _business.UpdateCategory(request);
        //    return CreateOkForResponse(product);
        //}
        [HttpDelete]
        [ProducesResponseType(typeof(ActionResponse), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            await _business.Delete(id);
            return NoContent();
        }
    }
}
