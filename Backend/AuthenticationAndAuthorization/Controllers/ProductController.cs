using AuthenticationAndAuthorization.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetProductByCode")]
        public async Task<IActionResult> GetProductByCode(string ProductCode)
        {
            return Ok();
        }
    }
}
