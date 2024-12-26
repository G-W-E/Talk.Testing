using System;

namespace Demo01.Services;

public interface IAuthenticationService
{
   Task<bool> ValidateUser(string userName, string password);
}
