using ACMEIndustries.Models;
using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public AdministrationController(IConfiguration config, IUserManager context, IMapper mapper)
        {
            _config = config;
            _userManager = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var context = HttpContext.Request.Headers;
            var users = _userManager.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            var user = _userManager.GetUserByEmailAddress(model.EmailAddress);

            if (user != null)
            {
                return Conflict();
            }

            user = new User
            {
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                Role = model.Role
            };

            await _userManager.UpdateUserAsync(user);

            return Ok(user);
        }
       
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserModel model)
        {
            var user = _userManager.GetUserByEmailAddress(model.EmailAddress);

            if (user == null)
            {
                return NotFound();
            }

            var updatedUser = _mapper.Map<User>(model);

            await _userManager.UpdateUserAsync(updatedUser);

            return Ok(model);
        }

          [HttpDelete]
          [Route("DeleteUser/{id}")]
          public async Task<IActionResult> DeleteUser(int id)
          {
              var user = _userManager.GetUserById(id);

                if (user == null)
                {
                  return NotFound();
                }

                await _userManager.DeleteUserAsync(id);

                return Ok();
          }

          [HttpPut]
          [Route("UploadImage/{id}")]
          public async Task<IActionResult> UploadImage(int id)
          {
            var user = _userManager.GetUserById(id);

              if (user == null)
              {
                  return NotFound();
              }

              return Ok(user);
          }

        [HttpPost]
        [Route("CreateRole")]
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

