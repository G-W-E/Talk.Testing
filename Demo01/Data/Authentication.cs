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
    // /// <summary>
    // /// UnSafe
    // /// </summary>
    // /// <param name="userName"></param>
    // /// <param name="password"></param>
    // /// <returns></returns>
    // public async Task<bool> ValidateUser(string userName, string password)
    // {
    //     if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
    //     {
    //         using (var sha256 = SHA256.Create())
    //         {
    //             byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
    //             string passwordHash = BitConverter.ToString(sha256.ComputeHash(passwordBytes)).Replace("-", "").ToLower();
    //             var userValid = this.dbContext.Database.SqlQueryRaw<UserModel>($"Select * from [User] where UserName ={userName} and Password ={passwordHash}").ToList();
    //             if (userValid != null && userValid.Count > 0)
    //             {
    //                 return await Task.FromResult<bool>(true);
    //             }
    //             return await Task.FromResult<bool>(false);
    //         }
    //     }
    //     return await Task.FromResult<bool>(false);
    // }
    public async Task<bool> ValidateUser(string userName, string password)
    {
        try
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                string passwordHash = BitConverter.ToString(sha256.ComputeHash(passwordBytes)).Replace("-", "").ToLower();
                var result = this.dbContext.Authens
                                        .FromSqlInterpolated($"EXEC ValidateUser {userName}, {passwordHash}")
                                        .AsEnumerable()
                                        .Select(u => u.IsValid).FirstOrDefault();
                if (result == 1)
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
        catch (System.Exception)
        {
            throw;
        }
    }
}
