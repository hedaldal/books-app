using Books.Api.Models;

namespace Books.Api.Repositories;

public sealed class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = [];
    private readonly object _lock = new();

    public IReadOnlyList<Book> List()
    {
        lock (_lock)
        {
            return _books.Select(Clone).ToList();
        }
    }

    public Book? Get(Guid id)
    {
        lock (_lock)
        {
            var found = _books.FirstOrDefault(b => b.Id == id);
            return found is null ? null : Clone(found);
        }
    }

    public void Add(Book book)
    {
        lock (_lock)
        {
            _books.Add(Clone(book));
        }
    }

    public bool Delete(Guid id)
    {
        lock (_lock)
        {
            var idx = _books.FindIndex(b => b.Id == id);
            if (idx < 0)
            {
                return false;
            }

            _books.RemoveAt(idx);
            return true;
        }
    }

    public bool UpdateRatingAndComments(Guid id, int? rating, string? comments)
    {
        lock (_lock)
        {
            var found = _books.FirstOrDefault(b => b.Id == id);
            if (found is null)
            {
                return false;
            }

            found.Rating = rating;
            found.Comments = comments;
            return true;
        }
    }

    private static Book Clone(Book source)
    {
        return new Book
        {
            Id = source.Id,
            Title = source.Title,
            Author = source.Author,
            Isbn = source.Isbn,
            CoverImageUrl = source.CoverImageUrl,
            Rating = source.Rating,
            Comments = source.Comments
        };
    }
}
