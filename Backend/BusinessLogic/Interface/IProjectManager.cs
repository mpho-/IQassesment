using ACMEIndustries.Models;

namespace BusinessLogic.Interface
{
    public interface IProjectManager
    {
        Task AddProject(string roleName);
        List<Project>? GetProjects();
    }
}