using AuthenticationService.Controllers;
using AuthenticationService.DTO;
using AuthenticationService.Models;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AuthenticationService.Testing
{
    [TestFixture]
    public class Tests
    {
        private  AuthController _authController;
        private Mock<IAuthRepository> _authRepository;

        [SetUp]
        public void Setup()
        {
            _authRepository = new Mock<IAuthRepository>();
            _authController = new AuthController(_authRepository.Object);
        }

        [Test]
        [TestCase("sm123@gmail.com", "hello")]
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "hello")]
        [TestCase("sm123@gmail.com", "")]
        [TestCase("", "hello")]
        [TestCase("", "")]
        public void Login_ReturnsUnauthorized_OnNullorWrongCredentials(string email, string password)
        {
            //Arrange
            LoginModel details = new LoginModel();
            

            details.Email = email;
            details.Password = password;

            //Act
            var result = _authController.Login(details);

            //Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
        }

        [Test]
        [TestCase("abc@gmail.com", "123")]
        [TestCase("xyz@gmail.com", "123")]
        [TestCase("lmn@gmail.com", "123")]
        public void Login_ReturnsSuccessObject_OnValidData(string email, string password)
        {
            //Arrange
            LoginModel details = new LoginModel();

            details.Email = email;
            details.Password = password;

            AuthenticatedUserDTO user = new AuthenticatedUserDTO
            {
                MemberId = 1,
                Name = "name",
                Token = "token"
            };
            _authRepository.Setup(x => x.Login(It.IsAny<LoginModel>())).Returns(user);

            //Act
            var result = _authController.Login(details);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}