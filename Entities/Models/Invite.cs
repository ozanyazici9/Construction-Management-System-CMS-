namespace Entities.Models;

public class Invite
{
    public int id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public int RoleId { get; set; }
    public string TokenHash { get; set; } = null!;
    public int InvitedByUserId { get; set; }
    public User InvitedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
}
