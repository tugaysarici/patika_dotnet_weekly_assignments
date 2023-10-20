using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1, "Test", 1, 1)]
        [InlineData(1, "Tst", 1, 1)]
        [InlineData(1, "Test", 0, 1)]
        [InlineData(1, "Test", 1, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int bookId, string title, int genreId, int authorId)
        {
            var model = new UpdateBookCommand.UpdateBookModel { Title = title, GenreId = genreId, AuthorId = authorId };
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1, "Testtt", 3, 2)]
        [InlineData(2, "Tessst", 2, 3)]
        [InlineData(3, "Teeeeeest", 1, 1)]
        [InlineData(1, "Test test", 1, 3)]
        public void WhenValidInputsGiven_Validator_ShouldNotReturnErrors(int bookId, string title, int genreId, int authorId)
        {
            var model = new UpdateBookCommand.UpdateBookModel { Title = title, GenreId = genreId, AuthorId = authorId };
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().Be(0);
        }
        
    }
}