using System.ComponentModel.DataAnnotations;

namespace RStore.Api.Dto.User;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}