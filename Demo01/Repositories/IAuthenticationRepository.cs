using System;

namespace Demo01.Repositories;

public interface IAuthenticationRepository
{
    Task<bool> ValidateUser(string userName, string password);
}
