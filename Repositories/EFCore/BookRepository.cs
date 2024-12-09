using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await FindAll(trackChanges)
                .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)
                .Search(bookParameters.SearchTerm)
                .OrderBy(x => x.Id)
                .ToListAsync();

            return PagedList<Book>
                .ToPagedList(books,
                bookParameters.PageNumber,
                bookParameters.PageSize);
        }
        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(x => x.Id == id, trackChanges)
            .SingleOrDefaultAsync();
        public void CreateOneBook(Book book) => Create(book);
        public void UpdateOneBook(Book book) => Update(book);
        public void DeleteOneBook(Book book) => Delete(book);

    }
}
