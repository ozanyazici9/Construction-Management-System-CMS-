using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public abstract record UserForManipulationDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}
