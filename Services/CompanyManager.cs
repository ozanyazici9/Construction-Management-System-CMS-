using System.Dynamic;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CompanyManager : ICompanyService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;
    private readonly IDataShaper<CompanyDto> _shaper;

    public CompanyManager(IRepositoryManager manager, IMapper mapper, IDataShaper<CompanyDto> shaper)
    {
        _manager = manager;
        _mapper = mapper;
        _shaper = shaper;
    }

    public async Task<CompanyDto> CreateOneCompanyAsync(
        CompanyForInsertionDto companyForInsertionDto
    )
    {
        var company = _mapper.Map<Company>(companyForInsertionDto);
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

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
    {
        return await _manager.Company.GetAllCompaniesAsync(trackChanges);
    }

    public async Task<(IEnumerable<ExpandoObject> companies, MetaData metaData)> GetAllCompaniesAsync(
        CompanyParameters companyParameters,
        bool trackChanges
    )
    {
        var companiesWithMetadata = await _manager.Company.GetAllCompaniesAsync(
            companyParameters,
            trackChanges
        );

        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companiesWithMetadata);
        var shapedCompanies =  _shaper.ShapeData(companiesDto, companyParameters.Fields);

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
        _manager.Company.UpdateOneCompany(company);
        await _manager.SaveAsync();
    }

    public async Task UpdateOneCompanyAsync(
        int id,
        CompanyForUpdateDto companyForUpdateDto,
        bool trackChanges
    )
    {
        var company = await GetOneCompanyAndCheckExists(id, trackChanges);

        _manager.Company.UpdateOneCompany(_mapper.Map(companyForUpdateDto, company));
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
