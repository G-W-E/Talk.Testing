using System;
using Demo01.Models;

namespace Demo01.Data;

public interface IAuthentication
{
    Task<bool> ValidateUser(string userName, string password);
}
