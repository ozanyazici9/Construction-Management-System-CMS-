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


    public ICompanyRepository Company => throw new NotImplementedException();

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
