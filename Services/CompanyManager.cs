using System.Dynamic;
using AutoMapper;
using Entities.Constants;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CompanyManager : ICompanyService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IDataShaper<CompanyDto> _shaper;

    public CompanyManager(
        IRepositoryManager repositoryManager,
        UserManager<User> userManager,
        IMapper mapper,
        IDataShaper<CompanyDto> shaper
    )
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _mapper = mapper;
        _shaper = shaper;
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

    public async Task DeleteOneCompanyAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _repositoryManager.Company.DeleteOneCompany(company);
        await _repositoryManager.SaveAsync();
    }

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
    {
        return await _repositoryManager.Company.GetAllCompaniesAsync(trackChanges);
    }

    public async Task<(
        IEnumerable<ExpandoObject> companies,
        MetaData metaData
    )> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges)
    {
        var companiesWithMetadata = await _repositoryManager.Company.GetAllCompaniesAsync(
            companyParameters,
            trackChanges
        );

        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companiesWithMetadata);
        var shapedCompanies = _shaper.ShapeData(companiesDto, companyParameters.Fields);

        return (shapedCompanies, companiesWithMetadata.MetaData);
    }

    public async Task<CompanyDto> GetOneCompanyByIdAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<(
        CompanyForUpdateDto companyForUpdateDto,
        Company company
    )> GetOneCompanyForPatch(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);
        var companyForUpdateDto = _mapper.Map<CompanyForUpdateDto>(company);
        return (companyForUpdateDto, company);
    }

    public async Task SaveChangesForPatchAsync(
        CompanyForUpdateDto companyForUpdateDto,
        Company company
    )
    {
        _mapper.Map(companyForUpdateDto, company);
        _repositoryManager.Company.UpdateOneCompany(company);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateOneCompanyAsync(
        int id,
        CompanyForUpdateDto companyForUpdateDto,
        bool trackChanges
    )
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _repositoryManager.Company.UpdateOneCompany(_mapper.Map(companyForUpdateDto, company));
        await _repositoryManager.SaveAsync();
    }

    private async Task<Company> GetOneCompanyAndCheckExists(int id, bool trackChanges)
    {
        var company = await _repositoryManager.Company.GetOneCompanyById(id, trackChanges);

        if (company is null)
            throw new CompanyNotFoundException(id);

        return company;
    }
}
