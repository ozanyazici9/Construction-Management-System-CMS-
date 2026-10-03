using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public abstract record InvitationForManipulationDto
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; init; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; init; } = null!;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; init; } = null!;
}
