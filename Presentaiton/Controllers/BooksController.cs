using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentaiton.ActionFilter;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;
using static Entities.Exceptions.NotFoundException;

namespace Presentaiton.Controllers;


[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpHead]
    [HttpGet(Name = "GetAllBooksAsync")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetAllAsync([FromQuery] BookParameters bookParameters)
    {
        var linkParameters = new LinkParameters()
        {
            BookParameters = bookParameters,
            HttpContext = HttpContext
        };

        var result = await _manager
            .BookService
            .GetAllBooksAsync(linkParameters, false);

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks ?
            Ok(result.linkResponse.LinkedEntities) :
            Ok(result.linkResponse.ShapedEntities);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _manager.BookService.GetOneBookByIdAsync(id, false);

        return Ok(result);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost("add")]
    public async Task<IActionResult> CreateAsync(BookDtoForInsertion bookDto)
    {
        var book = await _manager.BookService.CreateOneBookAsync(bookDto);
        return StatusCode(201, book);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(int id, BookDtoForUpdate bookDto)
    {
        await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
        return NoContent();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _manager.BookService.DeleteOneBookAsync(id, false);

        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetBooksOptions()
    {
        Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
        return Ok();
    }
}

