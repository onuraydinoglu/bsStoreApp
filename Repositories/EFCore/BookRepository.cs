using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Book> GetAllBooks(bool trackChanges) => 
            FindAll(trackChanges)
            .OrderBy(x => x.Id);

        public Book GetOneBookById(int id, bool trackChanges) => 
            FindByCondition(x => x.Id == id, trackChanges)
            .SingleOrDefault();

        public void CreateOneBook(Book book) => Create(book);
        public void UpdateOneBook(Book book) => Update(book);
        public void DeleteOneBook(Book book) => Delete(book);

    }
}
