using AutoMapper;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/books")]
  public class BooksController : ControllerBase
  {
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BooksController(LibraryDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    // GET: api/v1/books
    [HttpGet]
    public async Task<ActionResult> GetBooks()
    {
      var books = await _context.Books.ToListAsync();
      var booksDto = _mapper.Map<List<BookDto>>(books);

      return Ok(new
      {
        ok = true,
        books = booksDto
      });
    }

    // GET: api/v1/books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult> GetBookById(Guid id)
    {
      var book = await _context.Books.FindAsync(id);
      if(book == null)
        return NotFound(new { ok = false, msg = "Libro no encontrado" });

      var bookDto = _mapper.Map<BookDto>(book);
      return Ok(new
      {
        ok = true,
        books = bookDto
      });
    }

    // POST: api/v1/books
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState
          .Where(e => e.Value?.Errors.Count > 0)
          .ToDictionary(
              e => e.Key,
              e => e.Value?.Errors.Select(err => err.ErrorMessage).ToArray()
          );

        return BadRequest(new { ok = false, errors});
      }

      var book = _mapper.Map<Book>(request);
      book.Id = Guid.NewGuid();
      book.Status = BookStatus.Available;

      _context.Books.Add(book);
      await _context.SaveChangesAsync();

      var bookDto = _mapper.Map<BookDto>(book);

      return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, new
      {
        ok = true,
        book = bookDto
      });
    }

    // PUT: api/v1/books/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookRequest request)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState
          .Where(e => e.Value?.Errors.Count > 0)
          .ToDictionary(
              e => e.Key,
              e => e.Value?.Errors.Select(err => err.ErrorMessage).ToArray()
          );

        return BadRequest(new { ok = false, errors });
      }

      if (id != request.Id)
        return BadRequest(new { ok = false, msg = $"El ID {id} no coincide con el cuerpo de la solicitud" });

      var existBook = await _context.Books.FindAsync(id);
      if (existBook is null)
        return NotFound(new { ok = false, msg = "Libro no encontrado" });

      _mapper.Map(request, existBook);
      await _context.SaveChangesAsync();

      return Ok(new { ok = true, msg = "Libro actualizado correctamente" });
    }

    // DELETE: api/v1/books/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book == null)
        return NotFound(new { ok = false, msg = "Libro no encontrado" });

      _context.Books.Remove(book);
      await _context.SaveChangesAsync();

      return Ok(new { ok = true, msg = "Libro eliminado correctamente" });
    }

    // PATCH: api/v1/books/{id}/borrow
    [HttpPatch("{id}/borrow")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BorrowBook(Guid id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book == null)
        return NotFound(new { ok = false, msg = "Libro no encontrado" });

      book.Status = BookStatus.Borrowed;
      await _context.SaveChangesAsync();

      return Ok(new { ok = true, msg = "Libro marcado como prestado" });
    }

    // PATCH: api/v1/books/{id}/return
    [HttpPatch("{id}/return")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book == null)
        return NotFound(new { ok = false, msg = "Libro no encontrado" });

      book.Status = BookStatus.Available;
      await _context.SaveChangesAsync();

      return Ok(new { ok = true, msg = "Libro marcado como disponible" });
    }    
  }
}