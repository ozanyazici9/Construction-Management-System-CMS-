namespace Entities.DataTransferObjects;

public record CreateInvitationRequest
{
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string Email { get; init; } = null!;
    public int RoleId { get; init; } 
}
