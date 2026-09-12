using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts;

public interface ICompanyRepository : IRepositoryBase<Company>
{
    Task<PagedList<Company>> GetAllCompaniesAsync(CompanyParameters bookParameters ,bool trackChanges);
    Task<Company> GetOneCompanyById(int id, bool trackChanges);
    void CreateOneCompany(Company company);
    void UpdateOneCompany(Company company);
    void DeleteOneCompany(Company company); 
}
