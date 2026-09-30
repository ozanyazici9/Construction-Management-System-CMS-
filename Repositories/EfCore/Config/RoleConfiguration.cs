using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EfCore.Config;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "SystemAdmin",
                NormalizedName = "SYSTEMADMIN",
                ConcurrencyStamp = "11111111-1111-1111-1111-111111111111",
            },
            new IdentityRole
            {
                Id = "2",
                Name = "CompanyAdmin",
                NormalizedName = "COMPANYADMIN",
                ConcurrencyStamp = "22222222-2222-2222-2222-222222222222",
            },
            new IdentityRole
            {
                Id = "3",
                Name = "ProjectManager",
                NormalizedName = "PROJECTMANAGER",
                ConcurrencyStamp = "33333333-3333-3333-3333-333333333333",
            },
            new IdentityRole
            {
                Id = "4",
                Name = "SiteManager",
                NormalizedName = "SITEMANAGER",
                ConcurrencyStamp = "44444444-4444-4444-4444-444444444444",
            },
            new IdentityRole
            {
                Id = "5",
                Name = "Engineer",
                NormalizedName = "ENGINEER",
                ConcurrencyStamp = "55555555-5555-5555-5555-555555555555",
            },
            new IdentityRole
            {
                Id = "6",
                Name = "Accountant",
                NormalizedName = "ACCOUNTANT",
                ConcurrencyStamp = "66666666-6666-6666-6666-666666666666",
            },
            new IdentityRole
            {
                Id = "7",
                Name = "Procurement",
                NormalizedName = "PROCUREMENT",
                ConcurrencyStamp = "77777777-7777-7777-7777-777777777777",
            },
            new IdentityRole
            {
                Id = "8",
                Name = "Warehouse",
                NormalizedName = "WAREHOUSE",
                ConcurrencyStamp = "88888888-8888-8888-8888-888888888888",
            },
            new IdentityRole
            {
                Id = "9",
                Name = "Viewer",
                NormalizedName = "VIEWER",
                ConcurrencyStamp = "99999999-9999-9999-9999-999999999999",
            }
        );
    }
}
