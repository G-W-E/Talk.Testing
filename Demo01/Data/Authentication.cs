using System;
using System.Security.Cryptography;
using Demo01.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Data;

public class Authentication : IAuthentication
{
    private readonly DemoDbContext dbContext;

    public Authentication(DemoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<bool> ValidateUser(string userName, string password)
    {
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                string passwordHash = BitConverter.ToString(sha256.ComputeHash(passwordBytes)).Replace("-", "").ToLower();
                var userValid = await this.dbContext.Database.ExecuteSqlInterpolatedAsync($"ValidateUser {userName}, {passwordHash}");
                if (userValid != null && userValid.ToString() == "1")
                {
                    return await Task.FromResult(true);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
        }
        return await Task.FromResult(false);
    }
}
