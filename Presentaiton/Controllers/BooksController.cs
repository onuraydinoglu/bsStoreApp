using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
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

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _manager.BookService.GetAllBooksAsync(false);
        return Ok(result);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _manager.BookService.GetOneBookByIdAsync(id, false);
        
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateAsync(BookDtoForInsertion bookDto)
    {
        if (bookDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var book = await _manager.BookService.CreateOneBookAsync(bookDto);

        return StatusCode(201, book);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(int id, BookDtoForUpdate bookDto)
    {
        if (bookDto is null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

        return NoContent();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _manager.BookService.DeleteOneBookAsync(id, false);

        return NoContent();
    }
}

