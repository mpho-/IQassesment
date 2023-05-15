using ACMEIndustries.Database;
using ACMEIndustries.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic
{
    public class UserManager : IUserManager
    {
        private IApplicationDbContext _context { get; set; }
        private readonly IConfiguration _configuration;
        public UserManager(IApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public User? GetUserById(int id)
        {
            return _context.Json.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAllUsers()
        {
            return _context.Json.Users.ToList();
        }

        public async Task UpdateUserAsync(User user)
        {
            var foundUser = _context.Json.Users
                 .Select((u, i) => new { user = u, index = i })
                 .FirstOrDefault(x => x.user.Id == user.Id);

            if (foundUser != null)
            {
                _context.Json.Users[foundUser.index] = user;
            }

            await _context.SaveContextAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var foundUser = _context.Json.Users
                 .FirstOrDefault(x => x.Id == id);

            if (foundUser != null)
            {
                _context.Json.Users.Remove(foundUser);
            }

            await _context.SaveContextAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            var foundUser = _context.Json.Users
                 .FirstOrDefault(x => x.EmailAddress == user.EmailAddress);

            if (foundUser == null)
            {
                user.Id = _context.Json.Users.Count() + 1;
                _context.Json.Users.Add(user);
            }

            await _context.SaveContextAsync();
        }

        public User? SignIn(string password, string emailAddress)
        {
            var foundUser = _context.Json.Users
                 .FirstOrDefault(x => x.EmailAddress == emailAddress && x.Password == password);

            return foundUser;
        }

        public User? GetUserByEmailAddress(string emailAddress)
        {
            return _context.Json.Users.FirstOrDefault(x => x.EmailAddress == emailAddress);
        }

        public JwtSecurityToken CreateToken(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, user.Role, ClaimValueTypes.String));

            claims.Add(new Claim(ClaimTypes.PrimarySid, user.Id.ToString(), ClaimValueTypes.Integer));
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName, ClaimValueTypes.String));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress ?? string.Empty, ClaimValueTypes.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return token;
        }
    }
}