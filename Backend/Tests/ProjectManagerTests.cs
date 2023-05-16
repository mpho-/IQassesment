using ACMEIndustries.Database;
using ACMEIndustries.Models;
using BusinessLogic;
using BusinessLogic.Interface;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace BusinessLogicTests
{
    [TestFixture]
    public class ProjectManagerTests
    {
        private IProjectManager _profileManager;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().Build();
            var context = new ApplicationDbContext();
            CreateContext(context);

            _profileManager = new ProjectManager(context, configuration);
        }

        [Test]
        public void GetAllProjects_ShouldReturnAllProjects()
        {
            // Act
            var result = _profileManager.GetProjects();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Project>), result);
            Assert.Greater(result.Count(), 0);
        }


        [Test]
        public async Task CreateProjectAsync_ShouldCreateProject()
        {
            // Arrange
            var project = new Project
            {
                Name = "John2"
            };

            // Act
            var oldProjects = _profileManager.GetProjects();
            await _profileManager.AddProject(project.Name);
            var newProjects = _profileManager.GetProjects();

            // Assert
            Assert.AreNotEqual(oldProjects.Count, newProjects.Count);
        }

        private void CreateContext(ApplicationDbContext jsonContext)
        {
            jsonContext.Json = new JsonDbContext()
            {
                Projects = new List<Project>
                {
                    new Project { Name = "Test", Id = 1}
                },
                Roles = new List<Role>(),
                Users = new List<User>()
            };
        }
    }
}
