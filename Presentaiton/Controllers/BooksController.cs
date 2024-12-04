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
    public IActionResult GetAll()
    {
        var result = _manager.BookService.GetAllBooks(false);
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetById(int id)
    {
        var result = _manager.BookService.GetOneBookById(id, false);
        
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Create(Book book)
    {
        if (book is null)
        {
            return BadRequest();
        }

        _manager.BookService.CreateOneBook(book);

        return StatusCode(201, book);
    }

    [HttpPut("update")]
    public IActionResult Update(int id, BookDtoForUpdate bookDto)
    {
        if (bookDto is null)
        {
            return BadRequest();
        }

        _manager.BookService.UpdateOneBook(id, bookDto, true);

        return NoContent();
    }

    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        _manager.BookService.DeleteOneBook(id, false);

        return NoContent();
    }
}

