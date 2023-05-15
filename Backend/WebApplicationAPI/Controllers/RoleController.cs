using ACMEIndustries.Models;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRoleManager _roleManager;
        private readonly IMapper _mapper;

        public RolesController(IConfiguration config, IRoleManager context, IMapper mapper)
        {
            _config = config;
            _roleManager = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var context = HttpContext.Request.Headers;
            var roles = _roleManager.GetRoles();
            return Ok(roles);
        }

        [HttpPost]
        [Route("CreateRole/{name}")]
        public async Task<IActionResult> CreateRole(string name)
        {
            _roleManager.AddRole(name);

            return Ok();
        }
    }
}

