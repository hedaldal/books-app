namespace Books.Api.Models;

public sealed class CreateBookRequest
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public string? Isbn { get; init; }
    public List<string>? CoverImageUrls { get; init; }
    public int? Rating { get; init; }
    public string? Comments { get; init; }
}
