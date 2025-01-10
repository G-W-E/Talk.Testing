using System;
using System.Security.Cryptography;
using Demo01.Data;
using Demo01.Models;
using Demo01.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Demo01.Test;
[TestFixture]
public class AuthenticationRepositoryTest
{
    private Mock<DemoDbContext> _dbContextMock;
    private Mock<DbSet<AuthenModel>> _authenDbSetMock;
    private AuthenticationRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dbContextMock = new Mock<DemoDbContext>();
        _authenDbSetMock = new Mock<DbSet<AuthenModel>>();

        _dbContextMock.Setup(db => db.Authens).Returns(_authenDbSetMock.Object);

        _repository = new AuthenticationRepository(_dbContextMock.Object);
    }
    [Test]
    public async Task ValidateUser_ValidCredentials_ReturnsTrue()
    {
        // Arrange
        var userName = "Admin";
        var password = "ValidPassword";

        var mockData = new List<AuthenModel>
    {
        new AuthenModel { IsValid = 1 }
    }.AsQueryable();

        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.Provider).Returns(mockData.Provider);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.Expression).Returns(mockData.Expression);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.ElementType).Returns(mockData.ElementType);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

        _dbContextMock.Setup(db => db.Authens.FromSqlInterpolated(It.IsAny<FormattableString>()))
                      .Returns(_authenDbSetMock.Object);

        // Act
        var result = await _repository.ValidateUser(userName, password);

        // Assert
        Assert.IsTrue(result);
    }
    [Test]
    public async Task ValidateUser_InvalidCredentials_ReturnsFalse()
    {
        // Arrange
        var userName = "InvalidUser";
        var password = "InvalidPassword";

        var mockData = new List<AuthenModel>
    {
        new AuthenModel { IsValid = 0 }
    }.AsQueryable();

        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.Provider).Returns(mockData.Provider);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.Expression).Returns(mockData.Expression);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.ElementType).Returns(mockData.ElementType);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

        _dbContextMock.Setup(db => db.Authens.FromSqlInterpolated(It.IsAny<FormattableString>()))
                      .Returns(_authenDbSetMock.Object);

        // Act
        var result = await _repository.ValidateUser(userName, password);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public async Task ValidateUser_EmptyCredentials_ReturnsFalse()
    {
        // Arrange
        var userName = string.Empty;
        var password = string.Empty;

        var mockData = new List<AuthenModel>
    {
        new AuthenModel { IsValid = 0 }
    }.AsQueryable();

        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.Provider).Returns(mockData.Provider);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.Expression).Returns(mockData.Expression);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.ElementType).Returns(mockData.ElementType);
        _authenDbSetMock.As<IQueryable<AuthenModel>>()
                        .Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

        _dbContextMock.Setup(db => db.Authens.FromSqlInterpolated(It.IsAny<FormattableString>()))
                      .Returns(_authenDbSetMock.Object);

        // Act
        var result = await _repository.ValidateUser(userName, password);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public void ValidateUser_Exception_ThrowsException()
    {
        // Arrange
        var userName = "Admin";
        var password = "Password";

        _dbContextMock.Setup(db => db.Authens.FromSqlInterpolated(It.IsAny<FormattableString>()))
                      .Throws(new Exception("Test Exception"));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await _repository.ValidateUser(userName, password);
        });
    }


}

