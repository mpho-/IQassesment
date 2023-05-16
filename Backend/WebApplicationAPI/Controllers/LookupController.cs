using AutoMapper;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILookUpManager _lookupManager;

        public LookupController(IConfiguration config, ILookUpManager context)
        {
            _config = config;
            _lookupManager = context;
        }

        [HttpGet]
        [Route("GetGenders")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _lookupManager.GetGenders();
            return Ok(roles);
        }
    }
}

