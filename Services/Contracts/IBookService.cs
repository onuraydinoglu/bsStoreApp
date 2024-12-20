using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts;

public interface IBookService
{
    Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges);

    Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);

    Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);

    Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges);

    Task DeleteOneBookAsync(int id, bool trackChanges);
}
