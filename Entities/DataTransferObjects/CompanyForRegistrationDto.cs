using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record CompanyForRegistrationDto
{
    [Required]
    public CompanyForInsertionDto Company { get; init; }

    [Required]
    public UserForOwnerRegistrationDto Owner { get; init; }
}
