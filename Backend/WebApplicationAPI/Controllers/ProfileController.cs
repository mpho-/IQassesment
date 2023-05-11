using ACMEIndustries.Models;
using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public ProfileController(IConfiguration config, IUserManager context, IMapper mapper)
        {
            _config = config;
            _userManager = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid);

            var profile = _userManager.GetUserById(int.Parse(userId.Value));

            if (profile == null)
            {
                return NotFound();
            }

            var currentUser = _mapper.Map<UserModel>(profile);

            return Ok(currentUser);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProfile(UserModel model)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);

            var profile = _userManager.GetUserById(int.Parse(userId.Value));

            if (profile == null)
            {
                return NotFound();
            }

            var updatedUser = _mapper.Map<User>(model);

            await _userManager.UpdateUserAsync(updatedUser);

            return Ok(model);
        }
    }
}

