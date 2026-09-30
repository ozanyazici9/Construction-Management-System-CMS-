using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Contracts;

namespace Repositories.EfCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly ICompanyRepository _companyRepository;

    public RepositoryManager(RepositoryContext context, ICompanyRepository companyRepository)
    {
        _context = context;
        _companyRepository = companyRepository;
    }

    public ICompanyRepository Company => _companyRepository;

    public Task<IDbContextTransaction> BeginTransactionAsync() =>
        _context.Database.BeginTransactionAsync();

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
