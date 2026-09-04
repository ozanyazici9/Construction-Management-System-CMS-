using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EfCore;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options) { }

    public DbSet<Company> Companies { get; set; }
}
