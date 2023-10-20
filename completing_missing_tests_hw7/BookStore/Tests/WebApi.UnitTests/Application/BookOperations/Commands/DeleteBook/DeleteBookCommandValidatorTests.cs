using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-11)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(11)]
        public void WhenValidInputsGiven_Validator_ShouldNotReturnErrors(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().Be(0);
        }
    }
}