using ACMEIndustries.Models;
using BusinessLogic;
using NUnit.Framework;

namespace BusinessLogicTests
{
    [TestFixture]
    public class UserManagerTests
    {
        private UserManager _userManager;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _userManager = new UserManager();
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

        [Test]
        public async Task CreateUserAsync_ShouldCreateUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                EmailAddress = "john@example.com",
                Password = "password"
            };

            // Act
            await _userManager.CreateUserAsync(user);
            var createdUser = _userManager.GetUserById(user.Id);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.AreEqual(user.FirstName, createdUser.FirstName);
            Assert.AreEqual(user.EmailAddress, createdUser.EmailAddress);
            Assert.AreEqual(user.Password, createdUser.Password);
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
    }
}
