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
    public class RolesManager : IRoleManager
    {
        private IApplicationDbContext _context { get; set; }
        private readonly IConfiguration _configuration;
        public RolesManager(IApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public List<Role>? GetRoles()
        {
            return _context.Json.Roles.ToList();
        }

        public async Task AddRole(string roleName)
        {
            var foundRole = _context.Json.Roles
                 .FirstOrDefault(x => x.Name == roleName);

            if (foundRole == null)
            {
                var createRole = new Role();
                createRole.Id = _context.Json.Roles.Count() + 1;
                createRole.Name = roleName;
                _context.Json.Roles.Add(createRole);
            }

            await _context.SaveContextAsync();
        }
    }
}