using System;
using System.ComponentModel.DataAnnotations;

namespace Demo01.Dtos;

public class LoginDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [MinLength(8)]
    public string? Password { get; set; }
}
