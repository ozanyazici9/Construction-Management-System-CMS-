using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private ICompanyService _companyService;

    public ServiceManager(ICompanyService companyService )
    {
        _companyService = companyService;
    }

    public ICompanyService Company => _companyService;
}
