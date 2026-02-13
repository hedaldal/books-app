using Books.Api.Models;

namespace Books.Api.Repositories;

public interface IBookRepository
{
    IReadOnlyList<Book> List();
    Book? Get(Guid id);
    void Add(Book book);
    bool UpdateRatingAndComments(Guid id, int? rating, string? comments);
    bool Delete(Guid id);
}
