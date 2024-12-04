using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repositories.EFCore.Config;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
            new Book { Id = 1, Title = "Lorem", Price = 150 },
            new Book { Id = 2, Title = "Lorem", Price = 100 },
            new Book { Id = 3, Title = "Lorem", Price = 75 }
        );
    }
}
