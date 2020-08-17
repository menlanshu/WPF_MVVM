using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using Models = SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    class AuthenticationServiceTests
    {
        // Define those variables for global test
        // Then we do not need create them again and again in methods
        Mock<IAccountService> _mockAccountService;
        Mock<IPasswordHasher> _mockPasswordHasher;
        AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();

            _authenticationService = new AuthenticationService(_mockPasswordHasher.Object, _mockAccountService.Object);
        }


        [Test]
        public async Task Login_SucessForUserThatAlreadyExists_ReturnExistingAccount()
        {
            // Arrange
            string expectedUserName = "SingletonSean";
            string password = "Test123";

            _mockAccountService.Setup(s => s.GetByUserName(expectedUserName))
                .ReturnsAsync(new Models.Account() { AccountHolder = new Models.User() { UserName = expectedUserName } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Success);

            // Act
            Models.Account account = await _authenticationService.Login(expectedUserName, password);

            // Assert
            string actualUserName = account.AccountHolder.UserName;
            Assert.AreEqual(expectedUserName, actualUserName);
        }

        [Test]
        public void Login_SucessForUserThatAlreadyExists_ReturnInvalidPasswordValidation()
        {
            // Arrange
            string expectedUserName = "SingletonSean";
            string password = "Test123";

            _mockAccountService.Setup(s => s.GetByUserName(expectedUserName))
                .ReturnsAsync(new Models.Account() { AccountHolder = new Models.User() { UserName = expectedUserName } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUserName, password));

            // Assert
            string actualUserName = exception.UserName;
            Assert.AreEqual(expectedUserName, actualUserName);
        }

        [Test]
        public void Login_SucessForUserNotAlreadyExists_ReturnInvalidPasswordValidation()
        {
            // Arrange
            string expectedUserName = "SingletonSean";
            string password = "Test123";

            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUserName, password));

            // Assert
            string actualUserName = exception.UserName;
            Assert.AreEqual(expectedUserName, actualUserName);
        }


        [Test]
        public async Task Register_WithPasswordNotMatching_ReturnPasswordDoNoMatch()
        {
            // Arrange
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";
            RegistrationResult expected = RegistrationResult.PasswordDoNotMatch;

            // Act
            RegistrationResult actual = await _authenticationService
                .Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);


            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnEmailAlreadyExists()
        {
            // Arrange
            string email = "test@gmail.com";
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Models.Account());

            // Act
            RegistrationResult actual = await _authenticationService
                .Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());


            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUserName_ReturnUsernameAlreadyExists()
        {
            // Arrange
            string username = "testuser";
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;
            _mockAccountService.Setup(s => s.GetByUserName(username)).ReturnsAsync(new Models.Account());

            // Act
            RegistrationResult actual = await _authenticationService
                .Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserNameMatchingPassword_ReturnSuccess()
        {
            // Arrange
            RegistrationResult expected = RegistrationResult.Success;

            // Act
            RegistrationResult actual = await _authenticationService
                .Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
