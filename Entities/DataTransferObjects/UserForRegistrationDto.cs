using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record UserForRegistrationDto : UserForManipulationDto
{
    [Required(ErrorMessage = "Role field is required")]
    public ICollection<string>? Roles { get; init; }
}
