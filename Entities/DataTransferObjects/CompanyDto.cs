namespace Entities.DataTransferObjects;

public record CompanyDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string? TaxNumber { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public string? Website { get; init; }
    public DateTime CreatedAt { get ; init; }
    public bool IsActive { get; init; }
}
