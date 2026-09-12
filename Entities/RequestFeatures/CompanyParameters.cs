namespace Entities.RequestFeatures;

public class CompanyParameters : RequestParameters
{
    public CompanyParameters()
    {
        OrderBy = "id";
    }

    public string? Searchterm { get; set; }
}
