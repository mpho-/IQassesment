using ACMEIndustries.Database;
using ACMEIndustries.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLogic
{
    public interface IUserManager
    {
        Task CreateUserAsync(User user);
        List<User> GetAllUsers();
        User? GetUserById(int id);
        Task UpdateUserAsync(User user);
        User? SignIn(string password, string emailAddress);
        Task DeleteUserAsync(int id);
        JwtSecurityToken CreateToken(User user);
        User? GetUserByEmailAddress(string emailAddress);
    }
}