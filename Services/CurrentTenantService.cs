using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services;

public class CurrentTenantService : ICurrentTenantService
{
    private readonly int _tenantId;

    public CurrentTenantService(IHttpContextAccessor httpContextAccessor)
    {
        var claimValue = httpContextAccessor.HttpContext.User.FindFirst("companyId")?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out var tenantId))
        {
            throw new UnauthorizedAccessException("TenantId (companyId) not found in token");
        }

        _tenantId = tenantId;
    }

    public int TenantId => _tenantId;
}
