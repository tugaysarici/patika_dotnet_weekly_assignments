using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturned()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 9; // there is not any book that has id=9 value

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found.");
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1; // there is a book that has id=2 value

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}