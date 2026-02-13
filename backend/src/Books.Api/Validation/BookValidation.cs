using Books.Api.Models;

namespace Books.Api.Validation;

public static class BookValidation
{
    public const int MaxBooks = 25;
    public const int MaxTitleLength = 200;
    public const int MaxAuthorLength = 100;
    public const int MaxIsbnLength = 32;
    public const int MaxCommentsLength = 1000;
    public const int MaxCoverImageUrlLength = 2048;

    public static Dictionary<string, string[]> ValidateCreate(CreateBookRequest request)
    {
        var errors = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        AddRequiredAndLength(errors, nameof(request.Title), request.Title, MaxTitleLength);
        AddRequiredAndLength(errors, nameof(request.Author), request.Author, MaxAuthorLength);
        AddRequiredAndLength(errors, nameof(request.Isbn), request.Isbn, MaxIsbnLength);
        ValidateCoverImageUrls(errors, request.CoverImageUrls);

        ValidateRatingAndComments(errors, request.Rating, request.Comments);

        return ToDictionary(errors);
    }

    public static Dictionary<string, string[]> ValidateUpdate(UpdateBookRequest request)
    {
        var errors = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        ValidateRatingAndComments(errors, request.Rating, request.Comments);

        return ToDictionary(errors);
    }

    private static void ValidateRatingAndComments(Dictionary<string, List<string>> errors, int? rating, string? comments)
    {
        if (rating is < 1 or > 5)
        {
            AddError(errors, "Rating", "Rating must be between 1 and 5.");
        }

        if (rating.HasValue && string.IsNullOrWhiteSpace(comments))
        {
            AddError(errors, "Comments", "Comments are required when rating is provided.");
        }

        if (!string.IsNullOrWhiteSpace(comments))
        {
            if (comments.Length > MaxCommentsLength)
            {
                AddError(errors, "Comments", $"Comments must be {MaxCommentsLength} characters or fewer.");
            }

            if (comments.Contains("horrible", StringComparison.OrdinalIgnoreCase))
            {
                AddError(errors, "Comments", "Comments must not contain prohibited language.");
            }
        }
    }

    private static void ValidateCoverImageUrls(Dictionary<string, List<string>> errors, List<string>? coverImageUrls)
    {
        if (coverImageUrls is null || coverImageUrls.Count == 0)
        {
            return;
        }

        foreach (var url in coverImageUrls)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                AddError(errors, "CoverImageUrls", "CoverImageUrls must not contain empty values.");
                continue;
            }

            var trimmed = url.Trim();
            if (trimmed.Length > MaxCoverImageUrlLength)
            {
                AddError(errors, "CoverImageUrls", $"Each cover image URL must be {MaxCoverImageUrlLength} characters or fewer.");
            }

            if (!Uri.TryCreate(trimmed, UriKind.Absolute, out _))
            {
                AddError(errors, "CoverImageUrls", "Each cover image URL must be a valid absolute URL.");
            }
        }
    }

    private static void AddRequiredAndLength(Dictionary<string, List<string>> errors, string field, string? value, int max)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            AddError(errors, field, $"{field} is required.");
            return;
        }

        if (value.Length > max)
        {
            AddError(errors, field, $"{field} must be {max} characters or fewer.");
        }
    }

    private static void AddError(Dictionary<string, List<string>> errors, string field, string message)
    {
        if (!errors.TryGetValue(field, out var list))
        {
            list = [];
            errors[field] = list;
        }

        list.Add(message);
    }

    private static Dictionary<string, string[]> ToDictionary(Dictionary<string, List<string>> errors)
    {
        return errors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray(), StringComparer.OrdinalIgnoreCase);
    }
}
