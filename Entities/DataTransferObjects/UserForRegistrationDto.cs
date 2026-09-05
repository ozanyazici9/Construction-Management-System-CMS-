using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record UserForRegistrationDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public int CompanyId { get; init; }
    public ICollection<string>? Roles { get; init; }
}
