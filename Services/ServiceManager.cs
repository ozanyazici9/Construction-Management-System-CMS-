using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly ICompanyService _companyService;
    private readonly IAuthenticationService _authenticationService;

    public ServiceManager(ICompanyService companyService, IAuthenticationService authenticationService)
    {
        _companyService = companyService;
        _authenticationService = authenticationService;
    }

    public ICompanyService CompanyService => _companyService;

    public IAuthenticationService AuthenticationService => _authenticationService;
}
