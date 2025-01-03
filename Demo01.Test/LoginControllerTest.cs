
using Demo01.Controllers;
using Demo01.Dtos;
using Demo01.Repositories;
using Demo01.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Demo01.Test;

[TestFixture]
public class LoginControllerTest
{
    private Mock<IAuthenticationService> authenticationService;
    private Mock<ILogger<LoginController>> _mockLogger;
    private LoginController _logincontroller = null!;


    [SetUp]
    public void SetUp()
    {
        authenticationService = new Mock<IAuthenticationService>();
        _mockLogger = new Mock<ILogger<LoginController>>();

        _logincontroller = new LoginController(authenticationService.Object, _mockLogger.Object);
    }
    [TearDown]
    public void TearDown()
    {
        // Dispose of _logincontroller if necessary
        if (_logincontroller is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Test]
    public async Task Index_GoTo_Home()
    {
        // Arrange
        // Act
        var result = _logincontroller.Index() as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ViewName);
    }
    [Test]
    public async Task Index_InValidLogin_ShouldRedirectToHomeIndex()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            UserName = "testUser",
            Password = "testPassword"
        };

        authenticationService
            .Setup(service => service.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        var result = _logincontroller.Index(loginDto) as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ViewName);
    }
    [Test]
    public void Index_ValidLogin_ShouldRedirectToHomeIndex()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            UserName = "Admin",
            Password = "ABC@1234abc!"
        };

        authenticationService
            .Setup(service => service.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = _logincontroller.Index(loginDto);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Index", redirectResult.ActionName);
        Assert.AreEqual("Home", redirectResult.ControllerName);
    }
    [Test]
    public void Index_ModelIsInvalid()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            UserName = null,
            Password = null
        };

        _logincontroller.ModelState.AddModelError("UserName", "UserName is not empty");
        _logincontroller.ModelState.AddModelError("Password", "Password is not empty");

        // Act
        var result = _logincontroller.Index(loginDto) as ViewResult;
        // Assert
        Assert.IsNotNull(result);

        Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public void Index_ValidLogin_NullException()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            UserName = "Admin",
            Password = "ABC@1234abc!"
        };

        authenticationService
            .Setup(service => service.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new NullReferenceException());

        // Act
        var result = _logincontroller.Index(loginDto) as ViewResult;
        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ViewName);
    }

}
