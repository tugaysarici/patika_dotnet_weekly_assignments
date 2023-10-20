using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperation.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturned()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 9; // there is not any author that has id=9 value

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found.");
        }

        [Fact]
        public void WhenValidInputIsGiven_Author_ShouldBeDeleted()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 4; // there is an author that has id=4 value and it has not any book record yet

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
            author.Should().BeNull();
        }
    }
}