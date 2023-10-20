using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-11)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(11)]
        public void WhenValidInputsGiven_Validator_ShouldNotReturnErrors(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().Be(0);
        }
    }
}