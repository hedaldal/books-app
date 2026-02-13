using Books.Api.Models;
using Books.Api.Validation;
using Xunit;

namespace Books.Api.Tests;

public sealed class BookValidationTests
{
    [Fact]
    public void ValidateCreate_Success()
    {
        var request = new CreateBookRequest
        {
            Title = "Dune",
            Author = "Frank Herbert",
            Isbn = "978-0441013593",
            Rating = 5,
            Comments = "Great book!"
        };

        var errors = BookValidation.ValidateCreate(request);

        Assert.Empty(errors);
    }

    [Fact]
    public void ValidateCreate_WhenRatingProvidedWithoutComments_ReturnsError()
    {
        var request = new CreateBookRequest
        {
            Title = "Dune",
            Author = "Frank Herbert",
            Rating = 5,
            Comments = "   "
        };

        var errors = BookValidation.ValidateCreate(request);

        Assert.True(errors.ContainsKey("Comments"));
    }

    [Fact]
    public void ValidateUpdate_WhenCommentsContainProhibitedWord_ReturnsError()
    {
        var request = new UpdateBookRequest
        {
            Rating = 3,
            Comments = "This was HORRIBLE"
        };

        var errors = BookValidation.ValidateUpdate(request);

        Assert.True(errors.ContainsKey("Comments"));
    }
}
