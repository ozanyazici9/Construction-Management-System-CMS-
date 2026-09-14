using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;
using WebApi.Extensions;

namespace Repositories.EfCore;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext context)
        : base(context) { }

    public void CreateOneCompany(Company company) => Create(company);

    public void DeleteOneCompany(Company company) => Delete(company);

    public async Task<PagedList<Company>> GetAllCompaniesAsync(
        CompanyParameters companyParameters,
        bool trackChanges
    )
    {
        var companies = await FindAll(trackChanges)
            .FilterCompanies(companyParameters.Country, companyParameters.City)
            .SearchCompanies(companyParameters.Searchterm)
            .Sort(companyParameters.OrderBy)
            .ToListAsync();

        return PagedList<Company>.ToPagedList(
            companies,
            companyParameters.PageNumber,
            companyParameters.PageSize
        );
    }

    public async Task<Company> GetOneCompanyById(int id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void UpdateOneCompany(Company company) => Update(company);
}
