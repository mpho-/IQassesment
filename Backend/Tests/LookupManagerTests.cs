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
        private ILookUpManager _lookUpManager;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().Build();
            var context = new ApplicationDbContext();
            CreateContext(context);

            _lookUpManager = new LookUpManager(context, configuration);
        }

        [Test]
        public void GetAllLookUp_ShouldReturnAllGenders()
        {
            // Act
            var result = _lookUpManager.GetGenders();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Gender>), result);
            Assert.Greater(result.Count(), 0);
        }

        private void CreateContext(ApplicationDbContext jsonContext)
        {
            jsonContext.Json = new JsonDbContext()
            {
                Projects = new List<Project>(),
                Genders = new List<Gender> 
                { 
                    new Gender { Name = "Test", Id = 1}
                },
                Roles = new List<Role>(),
                Users = new List<User>()
            };
        }
    }
}
