using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;
using static Entities.Exceptions.BadRequestException;
using static Entities.Exceptions.NotFoundException;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly IDataShaper<BookDto> _shaper;
    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<BookDto> shaper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
        _shaper = shaper;
    }

    public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
    {
        var entity = _mapper.Map<Book>(bookDto);
        _manager.Book.CreateOneBook(entity);
        await _manager.SaveAsync();
        return _mapper.Map<BookDto>(entity);
    }

    public async Task DeleteOneBookAsync(int id, bool trackChanges)
    {
        var result = await GetOneBookByIdAndCheckExists(id, trackChanges);
        _manager.Book.DeleteOneBook(result);
        await _manager.SaveAsync();
    }

    public async Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> 
        GetAllBooksAsync(BookParameters bookParameters,
        bool trackChanges)
    {
        if (!bookParameters.ValidPriceRange)
            throw new PriceOutofRangeBadRequestException();

        var booksWithMetaData = await _manager
            .Book
            .GetAllBooksAsync(bookParameters, trackChanges);

        var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

        var shaperData = _shaper.ShapeData(booksDto, bookParameters.Fields);
        return (books : shaperData, metaData :  booksWithMetaData.MetaData);
    }

    public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
    {
        var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        return _mapper.Map<BookDto>(book);
    }

    public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
    {
        var result = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

        // Mapper
        // result.Title = book.Title;
        // result.Price = book.Price;

        result = _mapper.Map<Book>(bookDto);

        _manager.Book.Update(result);
        await _manager.SaveAsync();
    }

    private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
    {
        var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

        if(entity is null)
            throw new BookNotFoundException(id);

        return entity;
    }

}
