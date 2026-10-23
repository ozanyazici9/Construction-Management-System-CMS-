using System.Dynamic;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface ICompanyService
{
    Task<(IEnumerable<ExpandoObject> companies, MetaData metaData)> GetAllCompaniesAsync(
        CompanyParameters companyParameters,
        bool trackChanges
    );

    Task<IdentityResult> RegisterCompanyAsync(CompanyForRegistrationDto companyForRegistrationDto);

    Task<CompanyDto> GetOneCompanyByIdAsync(int id, bool trackChanges);

    Task UpdateOneCompanyAsync(int id, CompanyForUpdateDto companyForUpdateDto, bool trackChanges);

    Task DeleteOneCompanyAsync(int id, bool trackChanges);

    Task<(CompanyForUpdateDto companyForUpdateDto, Company company)> GetOneCompanyForPatch(
        int id,
        bool trackChanges
    );

    Task SaveChangesForPatchAsync(CompanyForUpdateDto companyForUpdateDto, Company company);

    Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
}
