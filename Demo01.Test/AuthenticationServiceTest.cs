using System;
using Demo01.Data;
using Demo01.Repositories;
using Demo01.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Demo01.Test;

public class AuthenticationService_Test
{
    private string _userName = "Admin";
    private string _password = "ABC@1234abc!";
    Mock<IAuthenticationRepository> authenticationRepository;
    AuthenticationService authenticationService;
    [SetUp]
    public void SetUp()
    {
        authenticationRepository = new Mock<IAuthenticationRepository>();
        authenticationService = new AuthenticationService(authenticationRepository.Object);
    }
    [TestCase]
    public void ValidateUserUsernamIsNullFail()
    {
        // Arrange
        string userName = string.Empty;
        string password = _password;
        authenticationRepository.Setup(repository => repository.ValidateUser(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(false);
        // Act
        var result = authenticationService.ValidateUser(userName, password);
        // Assert
        Assert.IsFalse(result.Result);
    }

    [TestCase]
    public void ValidateUserPasswordIsNullFail()
    {
        // Arrange
        string userName = _userName;
        string password = string.Empty;
        authenticationRepository.Setup(repository => repository.ValidateUser(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(false);
        // Act
        var result = authenticationService.ValidateUser(userName, password);
        // Assert
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public void ValidateUserUserNameAndPasswordIsNullFail()
    {
        // Arrange
        string userName = string.Empty;
        string password = string.Empty;
        authenticationRepository.Setup(repository => repository.ValidateUser(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(false);
        // Act
        var result = authenticationService.ValidateUser(userName, password);
        // Assert
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public void ValidateUserIncorrectFail()
    {
        // Arrange
        string userName = _userName + "123";
        string password = _password + "123";
        authenticationRepository.Setup(repository => repository.ValidateUser(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(false);
        // Act
        var result = authenticationService.ValidateUser(userName, password);
        // Assert
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public async Task ValidateUserCorrectSuccess()
    {
        // Arrange
        string userName = _userName;
        string password = _password;
        authenticationRepository.Setup(repository =>repository.ValidateUser(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await authenticationService.ValidateUser(userName, password);
        // Assert
        Assert.IsTrue(result);
    }

      [TestCase]
    public void ValidateUserThrowException()
    {
        // Arrange
        string userName = _userName + "123";
        string password = _password + "123";
        authenticationRepository.Setup(repository => repository.ValidateUser(It.IsAny<string>(),It.IsAny<string>())).Throws(new NullReferenceException());
        // Act
       
        // Assert
        Assert.ThrowsAsync<NullReferenceException>(async () =>{
            await authenticationService.ValidateUser(userName,password);
        });
    }
    
}
