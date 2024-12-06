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

    public BookDto CreateOneBook(BookDtoForInsertion bookDto)
    {
        var entity = _mapper.Map<Book>(bookDto);
        _manager.Book.CreateOneBook(entity);
        _manager.Save();
        return _mapper.Map<BookDto>(entity);
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

    public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
    {
        var books = _manager.Book.GetAllBooks(trackChanges);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public BookDto GetOneBookById(int id, bool trackChanges)
    {
        var book = _manager.Book.GetOneBookById(id, trackChanges);
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }
        return _mapper.Map<BookDto>(book);
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
