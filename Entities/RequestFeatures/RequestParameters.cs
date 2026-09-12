namespace Entities.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 50;
    public int PageNumber { get; set; }
    public string? OrderBy { get; set; }
    public string? Fields { get; set; }

    private int _pageSize = 10;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > maxPageSize ? maxPageSize : value; }
    }
}
