using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EfCore.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = 1,
            CompanyId = 1,
            FirstName = "System",
            LastName = "Admin",
            IsActive = true,
            CreatedAt = new DateTime(2026, 10, 3),

            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,

            PasswordHash = "AQAAAAIAAYagAAAAEGLjnwxdOjZoUf/P77ti+kRcp5qSqgr9nY76IGxpjcKA5pQaQvpkyyYlvbYI5Che5Q==",
            SecurityStamp = "7c1f3b0e-5a2d-4e8b-9c64-1d2a3b4c5d6e",
            ConcurrencyStamp = "b2e4a6c8-1f3d-4a5b-8c7e-9d0f1a2b3c4d",

            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        });
    }
}
