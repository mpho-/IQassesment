using ACMEIndustries.Database;
using ACMEIndustries.Models;
using BusinessLogic.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic
{
    public class LookUpManager : ILookUpManager
    {
        private IApplicationDbContext _context { get; set; }
        private readonly IConfiguration _configuration;
        public LookUpManager(IApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public List<Gender>? GetGenders()
        {
            return _context.Json.Genders.ToList();
        }

    }
}