using System;
using System.Security.Cryptography;
using Demo01.Data;
using Demo01.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Demo01.Test;

public class AuthenticationRepositoryTest
{
    private AuthenticationRepository authentication;
    private Mock<DemoDbContext> dbcontext;
    [SetUp]
    public void SetUp()
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configuration.GetConnectionString("EduConnect");
        var options = new DbContextOptionsBuilder<DemoDbContext>()
           .UseSqlServer(connectionString)
           .Options;
        dbcontext = new Mock<DemoDbContext>(options);
        authentication = new AuthenticationRepository(dbcontext.Object);
    }
    [TestCase]
    public  void ValidateUserUsernameIsEmptyFail()
    {
        string userName = string.Empty;
        string password = "ABC@1234abc!";
        var result =  authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public void ValidateUserUserNameAndPasswordIsEmptyFail()
    {
        string userName = string.Empty;
        string password = string.Empty;
        var result = authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public void ValidateUserPasswordIsEmptyFail()
    {
        string userName = "Admin";
        string password = string.Empty;
        var result = authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public void ValidateUserUserIncorrectFail()
    {
        string userName = "Admin1";
        string password = "ABC@1234abc!";
        var result = authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public void ValidateUserPasswordIncorrectFail()
    {
        string userName = "Admin";
        string password = "ABC@1234abc!w4343";
        var result = authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsFalse(result.Result);
    }
    [TestCase]
    public async Task ValidateUserSuccess()
    {
        string userName = "Admin";
        string password = "ABC@1234abc!";
        var result = await authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsTrue(result);
    }
    [TestCase]
    public async Task ValidateUserException()
    {
        string userName = "Admin";
        string password = "ABC@1234abc!";
        var result = await authentication.ValidateUser(userName, ShaPassword(password));
        Assert.IsTrue(result);
    }
    private string ShaPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            string passwordHash = BitConverter.ToString(sha256.ComputeHash(passwordBytes)).Replace("-", "").ToLower();
            return passwordHash;
        }
    }

}

