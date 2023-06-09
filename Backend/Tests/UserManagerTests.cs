using ACMEIndustries.Database;
using ACMEIndustries.Models;
using BusinessLogic;
using BusinessLogic.Interface;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Data;

namespace BusinessLogicTests
{
    [TestFixture]
    public class UserManagerTests
    {
        private IUserManager _userManager;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().Build();
            var context = new ApplicationDbContext();
            CreateContext(context);

            _userManager = new UserManager(context, configuration);
        }

        [Test]
        public void GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = _userManager.GetUserById(0);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Act
            var result = _userManager.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<User>), result);
        }

        [Test]
        public async Task UpdateUserAsync_ShouldUpdateUser()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                EmailAddress = "john@example.com",
                Password = "password"
            };

            // Act
            await _userManager.UpdateUserAsync(user);
            var updatedUser = _userManager.GetUserById(user.Id);

            // Assert
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(user.FirstName, updatedUser.FirstName);
            Assert.AreEqual(user.EmailAddress, updatedUser.EmailAddress);
            Assert.AreEqual(user.Password, updatedUser.Password);
        }


        [Test]
        public async Task CreateUserAsync_ShouldCreateUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                EmailAddress = "john2@example.com",
                Password = "password"
            };

            // Act
            var oldUsers = _userManager.GetAllUsers();
            await _userManager.CreateUserAsync(user);
            var newUsers = _userManager.GetAllUsers();

            // Assert
            Assert.AreNotEqual(oldUsers.Count, newUsers.Count);
        }

        [Test]
        public void SignIn_ShouldReturnUser_WhenUserExistsWithGivenCredentials()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                EmailAddress = "john@example.com",
                Password = "password"
            };
            _userManager.CreateUserAsync(user).Wait();

            // Act
            var result = _userManager.SignIn(user.Password, user.EmailAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.EmailAddress, result.EmailAddress);
            Assert.AreEqual(user.Password, result.Password);
        }

        [Test]
        public void SignIn_ShouldReturnNull_WhenUserDoesNotExistWithGivenCredentials()
        {
            // Act
            var result = _userManager.SignIn("invalid_password", "invalid_email_address");

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                EmailAddress = "john@example.com",
                Password = "password"
            };
            await _userManager.CreateUserAsync(user);

            // Act
            await _userManager.DeleteUserAsync(user.Id);
            var deletedUser = _userManager.GetUserById(user.Id);

            // Assert
            Assert.IsNull(deletedUser);
        }

        private void CreateContext(ApplicationDbContext jsonContext)
        {
            jsonContext.Json = new JsonDbContext()
            {
                Users = new List<User>
                {
                    new User
                    {
                        Id = 1,
                        FirstName = "John4",
                        EmailAddress = "john4@example.com",
                        Password = "password"
                    }
                }
            };
        }
    }
}
