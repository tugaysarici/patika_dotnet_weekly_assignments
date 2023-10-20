using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperation.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            var author = new Author() { Name="Tugay", Surname="Sarıcı", BirthDate=new DateTime(1923,10,29) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel(){ Name = author.Name, Surname = author.Surname, BirthDate = author.BirthDate};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            CreateAuthorModel model = new CreateAuthorModel() { Name="Tugay", Surname="Sarıcı", BirthDate=new DateTime(1923,10,29) };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && 
                                                          author.Surname == model.Surname && 
                                                          author.BirthDate == model.BirthDate);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}