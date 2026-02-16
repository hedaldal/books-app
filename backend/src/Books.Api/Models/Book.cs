namespace Books.Api.Models;

public sealed class Book
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public int? Rating { get; set; }
    public string? Comments { get; set; }
}
