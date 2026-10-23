using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
}
