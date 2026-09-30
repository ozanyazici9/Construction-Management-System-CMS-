using Microsoft.EntityFrameworkCore.Storage;

namespace Repositories.Contracts;

public interface IRepositoryManager
{
    ICompanyRepository Company { get; }
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
