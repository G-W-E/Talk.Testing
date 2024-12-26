using System.Security.Cryptography;
using Demo01.Repositories;

namespace Demo01.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationRepository authenticationRepository;

    public AuthenticationService(IAuthenticationRepository authenticationRepository)
    {
        this.authenticationRepository = authenticationRepository;
    }
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
                    var result = await this.authenticationRepository.ValidateUser(userName, passwordHash);
                    return await Task.FromResult(result);
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
