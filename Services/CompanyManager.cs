using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CompanyManager : ICompanyService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public CompanyManager(IRepositoryManager manager , IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<CompanyDto> CreateOneCompanyAsync(CompanyForInsertionDto companyForInsertionDto)
    {
        var company =_mapper.Map<Company>(companyForInsertionDto);
        _manager.Company.CreateOneCompany(company);
        await _manager.SaveAsync();

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task DeleteOneCompanyAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _manager.Company.DeleteOneCompany(company);
        await _manager.SaveAsync();
    }

    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companies = await _manager.Company.GetAllCompaniesAsync(trackChanges);

        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<CompanyDto> GetOneCompanyByIdAsync(int id, bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task UpdateOneCompanyAsync(int id,  CompanyForUpdateDto companyForUpdateDto,bool trackChanges)
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _manager.Company.UpdateOneCompany(company);
        await _manager.SaveAsync();
    }

    private async Task<Company> GetOneCompanyAndCheckExists(int id, bool trackChanges)
    {
        var company = await _manager.Company.GetOneCompanyById(id, trackChanges);

        if (company is null)
            throw new CompanyNotFoundException(id);

        return company;
    }
}
