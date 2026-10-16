using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record InvitationForUsersDto : InvitationForManipulationDto
{
    [Required(ErrorMessage = "Role field is required")]
    public ICollection<string>? Roles { get; init; }
}
