using Entities.Models;

namespace Services.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
    Task<Company> GetOneCompanyByIdAsync(int id, bool trackChanges);
    Task<Company> CreateOneCompanyAsync(Company company);
    Task UpdateOneCompanyAsync(int id, bool trackChanges);
    Task DeleteOneCompanyAsync(int id, bool trackChanges);
}
