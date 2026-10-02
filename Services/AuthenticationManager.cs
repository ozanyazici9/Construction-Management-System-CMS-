using AutoMapper;
using Entities.Constants;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class AuthenticationManager : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    private User? _user;

    public AuthenticationManager(
        UserManager<User> userManager,
        IMapper mapper,
        ILoggerService logger,
        IRepositoryManager repositoryManager
    )
    {
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
        _repositoryManager = repositoryManager;
    }

    public async Task<IdentityResult> RegisterCompanyAsync(
        CompanyForRegistrationDto companyForRegistrationDto
    )
    {
        var company = _mapper.Map<Company>(companyForRegistrationDto.Company);
        var user = _mapper.Map<User>(companyForRegistrationDto.Owner);

        await using var transaction = await _repositoryManager.BeginTransactionAsync();

        _repositoryManager.Company.CreateOneCompany(company);
        await _repositoryManager.SaveAsync(); // company.Id burada oluşuyor.

        user.CompanyId = company.Id;

        var result = await _userManager.CreateAsync(user, companyForRegistrationDto.Owner.Password);
        if (!result.Succeeded)
            return result; // commit edilmeden çıkınca transaction geri alındı.

        var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.CompanyAdmin);
        if (!result.Succeeded)
            return roleResult; // yine rollback

        await transaction.CommitAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
    {
        var user = _mapper.Map<User>(userForRegistrationDto);

        var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
        }

        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
    {
        _user = await _userManager.FindByNameAsync(userForAuthDto.UserName);
        var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password);
        if (!result)
        {
            _logger.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password");
        }
        return result;
    }
}
