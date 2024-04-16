using Assignment.Business.Abstractions;
using Assignment.Shared.Requests.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Mongo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase<IAuthBusiness>
    {
        public AuthController(IAuthBusiness business) : base(business)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _business.Login(request);
            return Ok(token);
        }
    }
}
