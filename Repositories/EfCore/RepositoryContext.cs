using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EfCore;

public class RepositoryContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
}
