namespace Entities.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Website { get; set; }
    public DateTime CreatedAt { get ; init; } = DateTime.UtcNow;
    public bool IsActive { get; set; }
}
