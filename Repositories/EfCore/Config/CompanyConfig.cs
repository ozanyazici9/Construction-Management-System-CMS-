using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EfCore.Config;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.TaxNumber).HasMaxLength(15);
        builder.Property(c => c.Email).HasMaxLength(150);
        builder.Property(c => c.Phone).HasMaxLength(20);
        builder.Property(c => c.Address).HasMaxLength(300);
        builder.Property(c => c.City).HasMaxLength(100);
        builder.Property(c => c.City).HasMaxLength(100);
        builder.Property(c => c.Country).HasMaxLength(100);
        builder.Property(c => c.Website).HasMaxLength(200);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.IsActive).IsRequired();

        builder.HasData(
            new Company
            {
                Id = 1,
                Name = "Türkiye Company",
                TaxNumber = "1111111111",
                Email = "turkiye@localhost",
                Phone = "1111111111",
                Address =
                    "Cumhuriyet Mahallesi, Atatürk Caddesi No: 24 Daire: 6, Kadıköy / İstanbul, 34710",
                City = "Istanbul",
                Country = "Türkiye",
                Website = "www.turkiye.com",
                CreatedAt = new DateTime(2023, 6, 15),
                IsActive = true,
            }
        );

        builder.HasData(
            new Company
            {
                Id = 2,
                Name = "Default Company",
                TaxNumber = "0000000000",
                Email = "default@localhost",
                Phone = "0000000000",
                Address = "350 5th Avenue, New York, NY 10118",
                City = "New York",
                Country = "United States",
                Website = "www.default.com",
                CreatedAt = new DateTime(2022, 12, 31),
                IsActive = true,
            }
        );
    }
}
