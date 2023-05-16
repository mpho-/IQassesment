using ACMEIndustries.Database;
using ACMEIndustries.Models;
using BusinessLogic;
using BusinessLogic.Interface;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace BusinessLogicTests
{
    [TestFixture]
    public class LookUpManagerTests
    {
        private IProjectManager _profileManager;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().Build();
            _profileManager = new ProjectManager(new ApplicationDbContext(), configuration);
        }

        [Test]
        public void GetAllProjects_ShouldReturnAllProjects()
        {
            // Act
            var result = _profileManager.GetProjects();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Project>), result);
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
    }
}
