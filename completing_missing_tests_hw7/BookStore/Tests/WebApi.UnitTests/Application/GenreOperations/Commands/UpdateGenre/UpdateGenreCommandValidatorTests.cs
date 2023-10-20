using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1, "Tst", true)]
        [InlineData(1, "Ts", true)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int genreId, string name, bool isActive)
        {
            var model = new UpdateGenreModel { Name = name, IsActive = isActive };
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1, "Test", true)]
        [InlineData(1, "Test", false)]
        public void WhenValidInputsGiven_Validator_ShouldNotReturnErrors(int genreId, string name, bool isActive)
        {
            var model = new UpdateGenreModel { Name = name, IsActive = isActive };
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().Be(0);
        }
    }
}