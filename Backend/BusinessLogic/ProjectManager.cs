using ACMEIndustries.Database;
using ACMEIndustries.Models;
using BusinessLogic.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic
{
    public class ProjectManager : IProjectManager
    {
        private IApplicationDbContext _context { get; set; }
        private readonly IConfiguration _configuration;
        public ProjectManager(IApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public List<Project>? GetProjects()
        {
            return _context.Json.Projects.ToList();
        }

        public async Task AddProject(string roleName)
        {
            var foundProject = _context.Json.Projects
                 .FirstOrDefault(x => x.Name == roleName);

            if (foundProject == null)
            {
                var createProject = new Project();
                createProject.Id = _context.Json.Projects.Count() + 1;
                createProject.Name = roleName;
                _context.Json.Projects.Add(createProject);
            }

            await _context.SaveContextAsync();
        }
    }
}