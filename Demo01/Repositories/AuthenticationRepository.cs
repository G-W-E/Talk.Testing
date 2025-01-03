using Demo01.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly DemoDbContext dbContext;

    public AuthenticationRepository(DemoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<bool> ValidateUser(string userName, string password)
    {
        try
        {
            var resultList = await this.dbContext.Authens
                                           .FromSqlInterpolated($"EXEC ValidateUser {userName}, {password}")
                                           .ToListAsync();

            var isValid = resultList.FirstOrDefault()?.IsValid;
            if (isValid == 1)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
