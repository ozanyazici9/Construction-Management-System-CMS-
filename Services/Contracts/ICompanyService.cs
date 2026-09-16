using System.Dynamic;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts;

public interface ICompanyService
{
    Task<(IEnumerable<ExpandoObject> companies, MetaData metaData)> GetAllCompaniesAsync(
        CompanyParameters companyParameters,
        bool trackChanges
    );
    Task<CompanyDto> GetOneCompanyByIdAsync(int id, bool trackChanges);
    Task<CompanyDto> CreateOneCompanyAsync(CompanyForInsertionDto companyForInsertionDto);
    Task UpdateOneCompanyAsync(int id, CompanyForUpdateDto companyForUpdateDto, bool trackChanges);
    Task DeleteOneCompanyAsync(int id, bool trackChanges);
    Task<(CompanyForUpdateDto companyForUpdateDto, Company company)> GetOneCompanyForPatch(
        int id,
        bool trackChanges
    );
    Task SaveChangesForPatchAsync(CompanyForUpdateDto companyForUpdateDto, Company company);
}
