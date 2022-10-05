using System.ComponentModel.DataAnnotations;

namespace RStore.Api.Dto.User;

public class RegisterUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required] 
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public string Role { get; set; }
}