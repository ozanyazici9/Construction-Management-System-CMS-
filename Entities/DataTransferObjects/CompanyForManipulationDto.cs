using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public abstract record CompanyForManipulationDto
{
    [Required(ErrorMessage = "Company name is a required field.")]
    [MaxLength(200, ErrorMessage = "Maximum length for the Name is 200 characters.")]
    [MinLength(2, ErrorMessage = "Minimum length for the Name is 2 characters.")]
    public string Name { get; init; } = null!;

    [MaxLength(15, ErrorMessage = "Maximum length for the Tax Number is 15 characters.")]
    public string? TaxNumber { get; init; }

    [MaxLength(150, ErrorMessage = "Maximum length for the Email is 150 characters.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string? Email { get; init; }

    [MaxLength(20, ErrorMessage = "Maximum length for the Phone is 20 characters.")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    public string? Phone { get; init; }

    [MaxLength(300, ErrorMessage = "Maximum length for the Address is 300 characters.")]
    public string? Address { get; init; }

    [MaxLength(100, ErrorMessage = "Maximum length for the City is 100 characters.")]
    public string? City { get; init; }

    [MaxLength(100, ErrorMessage = "Maximum length for the Country is 100 characters.")]
    public string? Country { get; init; }

    [MaxLength(200, ErrorMessage = "Maximum length for the Website is 200 characters.")]
    public string? Website { get; init; }

    [Required]
    public bool IsActive { get; init; }
}
