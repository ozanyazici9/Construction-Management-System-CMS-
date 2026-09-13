namespace Entities.RequestFeatures;

public class CompanyParameters : RequestParameters
{
    public CompanyParameters()
    {
        OrderBy = "id";
    }

    public string? Searchterm { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}
