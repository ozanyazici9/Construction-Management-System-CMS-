using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EfCore.Config;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasData(
            new IdentityUserRole<int> { UserId = 1, RoleId = 1 }, // admin -> SystemAdmin
            new IdentityUserRole<int> { UserId = 2, RoleId = 2 }, // companyAdmin
            new IdentityUserRole<int> { UserId = 3, RoleId = 2 } // companyAdmin
        ); 
    }
}
