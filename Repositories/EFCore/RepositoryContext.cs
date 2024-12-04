using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;

namespace Repositories.EFCore;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfig());
    }
}
