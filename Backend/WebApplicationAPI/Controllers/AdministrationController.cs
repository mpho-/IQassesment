using ACMEIndustries.Models;
using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
        private IHostingEnvironment _hostingEnvironment;

        public AdministrationController(IConfiguration config, IUserManager context, IMapper mapper, IHostingEnvironment env)
        {
            _config = config;
            _userManager = context;
            _mapper = mapper;
            _hostingEnvironment = env;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var context = HttpContext.Request.Headers;
            var users = _userManager.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = _userManager.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            var user = _userManager.GetUserByEmailAddress(model.EmailAddress);

            if (user != null)
            {
                return Conflict("User already exists!");
            }

            var createdUser = _mapper.Map<User>(model);

            await _userManager.CreateUserAsync(createdUser);

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
        public async Task<IActionResult> UploadImage(IFormFile file, int id)
        {
            var user = _userManager.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newFileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, @"Resources", newFileName);
                    user.ProfilePicture = newFileName;
                    var fs = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(fs);
                    await _userManager.UpdateUserAsync(user);
                    return Ok(new { FileName = newFileName });
                }
            }
            return BadRequest();
        }

        private async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}

