using ACMEIndustries.Models;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProjectManager _projectManager;
        private readonly IMapper _mapper;

        public ProjectsController(IConfiguration config, IProjectManager context, IMapper mapper)
        {
            _config = config;
            _projectManager = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetProjects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = _projectManager.GetProjects();
            return Ok(projects);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject(string name)
        {
            _projectManager.AddProject(name);

            return Ok();
        }
    }
}

