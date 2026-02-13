namespace Books.Api.Models;

public sealed class UpdateBookRequest
{
    public int? Rating { get; init; }
    public string? Comments { get; init; }
}
