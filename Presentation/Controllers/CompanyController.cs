using System.Text.Json;
using Entities.Constants;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[Authorize(Roles = RoleNames.SystemAdmin)]
[ServiceFilter(typeof(LogFilterAttribute))]
[ApiController]
[Route("api/{v:apiversion}/companies")]
[ApiExplorerSettings(GroupName = "v1")]
public class CompanyController : ControllerBase
{
    private readonly IServiceManager _service;

    public CompanyController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("register-company")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterCompany(
        [FromBody] CompanyForRegistrationDto companyForRegistrationDto
    )
    {
        var result = await _service.CompanyService.RegisterCompanyAsync(
            companyForRegistrationDto
        );

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpGet]
    [HttpHead]
    public async Task<IActionResult> GetAllCompanies(
        [FromQuery] CompanyParameters companyParameters
    )
    {
        var pagedResult = await _service.CompanyService.GetAllCompaniesAsync(
            companyParameters,
            false
        );
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.companies);
    }

    [HttpGet("{id:int}")]
    [HttpHead("{id:int}")]
    public async Task<IActionResult> GetOneCompanyAsync([FromRoute(Name = "id")] int id)
    {
        var company = await _service.CompanyService.GetOneCompanyByIdAsync(id, false);

        return Ok(company);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneCompanyAsync([FromRoute(Name = "id")] int id)
    {
        await _service.CompanyService.DeleteOneCompanyAsync(id, trackChanges: false);

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

        await _service.CompanyService.UpdateOneCompanyAsync(
            id,
            companyForUpdateDto,
            trackChanges: true
        );

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PartiallyUpdateOneCompanyAsync(
        [FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<CompanyForUpdateDto> companyPatch
    )
    {
        if (companyPatch is null)
            return BadRequest();

        var result = await _service.CompanyService.GetOneCompanyForPatch(id, false);

        companyPatch.ApplyTo(result.companyForUpdateDto);

        TryValidateModel(result.companyForUpdateDto);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.CompanyService.SaveChangesForPatchAsync(
            result.companyForUpdateDto,
            result.company
        );

        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetCompaniesOptions()
    {
        Response.Headers.Append(
            "Allow",
            "GET, PUT, CREATE, UPDATE, PATCH, POST, HEAD, OPTIONS, DELETE"
        );

        return Ok();
    }
}
