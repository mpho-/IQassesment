using ACMEIndustries.Database;
using ACMEIndustries.Models;
using BusinessLogic;
using BusinessLogic.Interface;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace BusinessLogicTests
{
    [TestFixture]
    public class RoleManagerTests
    {
        private IRoleManager _roleManager;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().Build();
            var context = new ApplicationDbContext();
            CreateContext(context);

            _roleManager = new RolesManager(context, configuration);
        }

        [Test]
        public void GetAllRoles_ShouldReturnAllRoles()
        {
            // Act
            var result = _roleManager.GetRoles();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Role>), result);
            Assert.Greater(result.Count(), 0);
        }


        [Test]
        public async Task CreateRoleAsync_ShouldCreateRole()
        {
            // Arrange
            var role = new Role
            {
                Name = "John5"
            };

            // Act
            var oldRoles = _roleManager.GetRoles();
            await _roleManager.AddRole(role.Name);
            var newRoles = _roleManager.GetRoles();

            // Assert
            Assert.AreNotEqual(oldRoles.Count, newRoles.Count);
        }

        private void CreateContext(ApplicationDbContext jsonContext)
        {
            jsonContext.Json = new JsonDbContext()
            {
                Roles = new List<Role>
                {
                    new Role { Name = "Test", Id = 1}
                },
                Users = new List<User>()
            };
        }
    }
}
