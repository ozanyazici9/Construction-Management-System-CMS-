using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EfCore;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext context)
        : base(context) { }

    public void CreateOneCompany(Company company) => Create(company);

    public void DeleteOneCompany(Company company) => Delete(company);

    public async Task<List<Company>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companies = await FindAll(trackChanges).ToListAsync();

        return companies;
    }

    public async Task<Company> GetOneCompanyById(int id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void UpdateOneCompant(Company company) => Update(company);
}
