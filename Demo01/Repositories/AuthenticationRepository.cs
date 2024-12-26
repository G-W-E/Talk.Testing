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
            var result = this.dbContext.Authens
                                            .FromSqlInterpolated($"EXEC ValidateUser {userName}, {password}")
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
        catch (System.Exception)
        {
            throw;
        }
    }
}
