using Entities.Models;

namespace Repositories.Extensions;

public static class CompanyRepositoyExtensions
{
    public static IQueryable<Company> FilterCompanies(
        this IQueryable<Company> companies,
        string? country = null,
        string? city = null
    )
    {
        if (!string.IsNullOrWhiteSpace(country))
            companies = companies.Where(c => c.Country == country);

        if (!string.IsNullOrEmpty(city))
            companies = companies.Where(c => c.City == city);

        return companies;
    }

    public static IQueryable<Company> SearchCompanies(
        this IQueryable<Company> companies,
        string? searchTerm
    )
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return companies;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return companies.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
    }
}
