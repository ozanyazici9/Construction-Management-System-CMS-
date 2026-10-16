using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
    Task<IdentityResult> RegisterCompanyAsync(CompanyForRegistrationDto companyForRegistrationDto);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto);
    Task<TokenDto> CreateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}
