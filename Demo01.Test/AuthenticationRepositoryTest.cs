using System;
using Demo01.Data;
using Demo01.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo01.Test;

public class AuthenticationRepositoryTest
{
    IAuthenticationRepository authentication;
    [SetUp]
    public void SetUp()
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configuration.GetConnectionString("EduConnect");
        var options = new DbContextOptionsBuilder<DemoDbContext>()
           .UseSqlServer(connectionString)
           .Options;
        authentication = new AuthenticationRepository(new DemoDbContext(options));
    }
    [TestCase]
    public void ValidateUserUsernameIsEmptyFail()
    {
        string userName = string.Empty;
        string password = "ABC@1234abc!";
        var result = authentication.ValidateUser(userName, password);
        Assert.IsFalse(result.Result);
    }
}
