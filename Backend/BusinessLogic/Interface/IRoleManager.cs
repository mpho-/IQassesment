using ACMEIndustries.Models;

namespace BusinessLogic.Interface
{
    public interface IRoleManager
    {
        Task AddRole(string roleName);
        List<Role>? GetRoles();
    }
}