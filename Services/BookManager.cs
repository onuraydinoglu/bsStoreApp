using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using static Entities.Exceptions.NotFoundException;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
    }

    public Book CreateOneBook(Book book)
    {
        _manager.Book.CreateOneBook(book);
        _manager.Save();
        return book;
    }

    public void DeleteOneBook(int id, bool trackChanges)
    {
        var result = _manager.Book.GetOneBookById(id, trackChanges);
        if (result is null) {
            throw new BookNotFoundException(id);
        }

        _manager.Book.DeleteOneBook(result);
        _manager.Save();
    }

    public IEnumerable<Book> GetAllBooks(bool trackChanges)
    {
        return _manager.Book.GetAllBooks(trackChanges);
    }

    public Book GetOneBookById(int id, bool trackChanges)
    {
        var book = _manager.Book.GetOneBookById(id, trackChanges);
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }
        return book;
    }

    public void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges)
    {
        var result = _manager.Book.GetOneBookById(id, trackChanges);
        if(result is null)
        {
            string msg = $"Book with id:{id} could not found.";
            _logger.LogInfo(msg);
            throw new Exception(msg);
        }

        // Mapper
        // result.Title = book.Title;
        // result.Price = book.Price;

        result = _mapper.Map<Book>(bookDto);

        _manager.Book.Update(result);
        _manager.Save();
    }
}
