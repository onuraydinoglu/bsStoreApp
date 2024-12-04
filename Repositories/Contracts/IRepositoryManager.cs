namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IBookRepository Book {  get; }
    void Save();
}
