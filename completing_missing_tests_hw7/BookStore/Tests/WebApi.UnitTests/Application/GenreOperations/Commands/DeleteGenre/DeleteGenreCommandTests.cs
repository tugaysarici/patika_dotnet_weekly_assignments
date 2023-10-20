using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturned()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 9; // there is not any genre that has id=9 value

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found.");
        }

        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1; // there is a genre that has id=1 value

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().BeNull();
        }
    }
}