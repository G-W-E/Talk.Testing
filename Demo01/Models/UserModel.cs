using System;

namespace Demo01.Models;

public class UserModel
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}
