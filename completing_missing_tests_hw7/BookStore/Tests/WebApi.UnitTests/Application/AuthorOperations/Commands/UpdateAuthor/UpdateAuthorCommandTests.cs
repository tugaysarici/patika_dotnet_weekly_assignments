using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperation.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            this.context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturned()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(context);
            command.AuthorId = 9;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(context);
            var author = new Author { Name = "Tst", Surname = "Tst", BirthDate = new DateTime(2000,10,10) };

            context.Authors.Add(author);
            context.SaveChanges();

            command.AuthorId = author.Id;
            UpdateAuthorModel model = new UpdateAuthorModel { Name = "Test", Surname = "Test", BirthDate = new DateTime(2001,10,10) };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updatedAuthor = context.Authors.SingleOrDefault(a => a.Id == author.Id);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(author.Name);
            updatedAuthor.Surname.Should().Be(author.Surname);
            updatedAuthor.BirthDate.Should().Be(author.BirthDate);
        }
    }
}