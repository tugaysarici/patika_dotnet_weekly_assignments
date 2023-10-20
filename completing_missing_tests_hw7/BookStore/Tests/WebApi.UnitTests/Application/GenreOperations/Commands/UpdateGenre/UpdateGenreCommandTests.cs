using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            this.context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturned()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(context);
            command.GenreId = 9;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(context);
            var genre = new Genre { Name = "Poety", IsActive = true};

            context.Genres.Add(genre);
            context.SaveChanges();

            command.GenreId = genre.Id;
            UpdateGenreModel model = new UpdateGenreModel { Name = "Poetry", IsActive = true };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updatedGenre = context.Genres.SingleOrDefault(g => g.Id == genre.Id);
            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(model.Name);
            updatedGenre.IsActive.Should().Be(model.IsActive);
        }
    }
}