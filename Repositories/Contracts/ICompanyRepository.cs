using Entities.Models;

namespace Repositories.Contracts;

public interface ICompanyRepository : IRepositoryBase<Company>
{
    Task<List<Company>> GetAllCompaniesAsync(bool trackChanges);
    Task<Company> GetOneCompanyById(int id, bool trackChanges);
    void CreateOneCompany(Company company);
    void UpdateOneCompany(Company company);
    void DeleteOneCompany(Company company); 
}
