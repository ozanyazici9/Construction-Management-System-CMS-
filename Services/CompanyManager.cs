using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CompanyManager : ICompanyService
{
    private readonly IRepositoryManager _manager;

    public CompanyManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Company> CreateOneCompanyAsync(Company company)
    {
        _manager.Company.CreateOneCompany(company);
        await _manager.SaveAsync();
        return company;
    }

    public async Task DeleteOneCompanyAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _manager.Company.DeleteOneCompany(company);
        await _manager.SaveAsync();
    }

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companies = await _manager.Company.GetAllCompaniesAsync(trackChanges);

        return companies;
    }

    public async Task<Company> GetOneCompanyByIdAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        return company;
    }

    public async Task UpdateOneCompanyAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _manager.Company.UpdateOneCompany(company);
        await _manager.SaveAsync();
    }

    private async Task<Company> GetOneCompanyAndCheckExists(int id, bool trackChanges)
    {
        var company = await _manager.Company.GetOneCompanyById(id, trackChanges);

        if (company is null)
            throw new CompanyNotFoundException(id);

        return company;
    }
}
