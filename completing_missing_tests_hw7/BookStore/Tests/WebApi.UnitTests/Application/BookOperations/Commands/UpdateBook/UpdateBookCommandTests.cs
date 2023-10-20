using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            this.context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturned()
        {
            UpdateBookCommand command = new UpdateBookCommand(context);
            command.BookId = 9;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(context);
            var book = new Book { Title = "Oblomv", GenreId = 1, AuthorId = 2, PageCount = 650, PublishDate = new DateTime(1859,1,1) };

            context.Books.Add(book);
            context.SaveChanges();

            command.BookId = book.Id;
            UpdateBookCommand.UpdateBookModel model = new UpdateBookCommand.UpdateBookModel { Title = "Oblomov", GenreId = 3, AuthorId = 4 };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updatedBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(book.Title);
            updatedBook.GenreId.Should().Be(model.GenreId);
            updatedBook.AuthorId.Should().Be(model.AuthorId);
            updatedBook.PageCount.Should().Be(book.PageCount);
            updatedBook.PublishDate.Should().Be(book.PublishDate);
        }
    }
}