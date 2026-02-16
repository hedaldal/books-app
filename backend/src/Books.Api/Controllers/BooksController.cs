using Books.Api.Models;
using Books.Api.Repositories;
using Books.Api.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/books")]
public sealed class BooksController(IBookRepository repository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Book>), StatusCodes.Status200OK)]
    public ActionResult<PagedResult<Book>> GetBooks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string sort = "asc")
    {
        if (page < 1 || pageSize < 1 || pageSize > 25)
        {
            return ValidationProblem(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                ["Paging"] = ["Page must be >= 1 and pageSize must be between 1 and 25."]
            }));
        }

        var query = repository.List().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(b =>
                b.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        query = sort.Equals("desc", StringComparison.OrdinalIgnoreCase)
            ? query.OrderByDescending(b => b.Title)
            : query.OrderBy(b => b.Title);

        var totalCount = query.Count();
        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(new PagedResult<Book>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Book> GetBook(Guid id)
    {
        var book = repository.Get(id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<Book> CreateBook([FromBody] CreateBookRequest request)
    {
        if (repository.List().Count >= BookValidation.MaxBooks)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Book limit reached",
                Detail = $"A maximum of {BookValidation.MaxBooks} books is allowed."
            });
        }

        var validationErrors = BookValidation.ValidateCreate(request);
        if (validationErrors.Count > 0)
        {
            return ValidationProblem(new ValidationProblemDetails(validationErrors));
        }

        var book = new Book
        {
            Title = request.Title!.Trim(),
            Author = request.Author!.Trim(),
            Isbn = request.Isbn!.Trim(),
            CoverImageUrl = string.IsNullOrWhiteSpace(request.CoverImageUrl) ? null : request.CoverImageUrl.Trim(),
            Rating = request.Rating,
            Comments = string.IsNullOrWhiteSpace(request.Comments) ? null : request.Comments.Trim()
        };

        repository.Add(book);

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Book> UpdateBook(Guid id, [FromBody] UpdateBookRequest request)
    {
        var validationErrors = BookValidation.ValidateUpdate(request);
        if (validationErrors.Count > 0)
        {
            return ValidationProblem(new ValidationProblemDetails(validationErrors));
        }

        var updated = repository.UpdateRatingAndComments(
            id,
            request.Rating,
            string.IsNullOrWhiteSpace(request.Comments) ? null : request.Comments.Trim());

        if (!updated)
        {
            return NotFound();
        }

        var book = repository.Get(id)!;
        return Ok(book);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBook(Guid id)
    {
        return repository.Delete(id) ? NoContent() : NotFound();
    }
}
