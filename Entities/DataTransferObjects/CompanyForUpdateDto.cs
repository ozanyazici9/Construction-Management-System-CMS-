using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record CompanyForUpdateDto : CompanyForManipulationDto
{
    [Required]
    public int Id { get; init; }
}
