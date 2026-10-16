namespace Services.Contracts;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    IAuthenticationService AuthenticationService { get; }
}
