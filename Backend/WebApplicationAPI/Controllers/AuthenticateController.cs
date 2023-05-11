using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Models;
using ACMEIndustries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public AuthenticateController(IUserManager context, IMapper mapper)
        {
            _userManager = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var user = _userManager.SignIn(model.Password, model.EmailAddress);

            if (user == null)
            {
                return Unauthorized();
            }
            var token = _userManager.CreateToken(user);

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), ExpiresIn = token.ValidTo, Role = user.Role});
        }

        [HttpGet]
        [Authorize]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = User.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToArray();

            return Ok(roles);
        }
    }
}
