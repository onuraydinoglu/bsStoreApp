using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) => 
            await FindAll(trackChanges)
            .OrderBy(x => x.Id).
            ToListAsync();

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(x => x.Id == id, trackChanges)
            .SingleOrDefaultAsync();

        public void CreateOneBook(Book book) => Create(book);
        public void UpdateOneBook(Book book) => Update(book);
        public void DeleteOneBook(Book book) => Delete(book);

    }
}
