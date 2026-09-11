using System.Text.Json;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[ServiceFilter(typeof(LogFilterAttribute))]
[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly IServiceManager _manager;

    public CompanyController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanies([FromQuery] CompanyParameters companyParameters)
    {
        var pagedResult = await _manager.Company.GetAllCompaniesAsync(companyParameters ,false);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.companies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneCompanyAsync([FromRoute(Name = "id")] int id)
    {
        var company = await _manager.Company.GetOneCompanyByIdAsync(id, false);

        return Ok(company);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateOneCompanyAsync(
        [FromBody] CompanyForInsertionDto companyForInsertionDto
    )
    {
        var entity = await _manager.Company.CreateOneCompanyAsync(companyForInsertionDto);

        return StatusCode(201, entity);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneCompanyAsync([FromRoute(Name = "id")] int id)
    {
        await _manager.Company.DeleteOneCompanyAsync(id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOneCompanyAsync(
        [FromRoute(Name = "id")] int id,
        [FromBody] CompanyForUpdateDto companyForUpdateDto
    )
    {
        if (id != companyForUpdateDto.Id)
            return BadRequest("Ids don't match.");

        await _manager.Company.UpdateOneCompanyAsync(id, companyForUpdateDto, trackChanges: true);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PartiallyUpdateOneCompanyAsync(
        [FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<CompanyForUpdateDto> companyPatch
    )
    {
        if(companyPatch is null)
            return BadRequest();

        var result = await _manager.Company.GetOneCompanyForPatch(id, false);

        companyPatch.ApplyTo(result.companyForUpdateDto);

        TryValidateModel(result.companyForUpdateDto);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _manager.Company.SaveChangesForPatchAsync(result.companyForUpdateDto, result.company);

        return NoContent();
    }
}
