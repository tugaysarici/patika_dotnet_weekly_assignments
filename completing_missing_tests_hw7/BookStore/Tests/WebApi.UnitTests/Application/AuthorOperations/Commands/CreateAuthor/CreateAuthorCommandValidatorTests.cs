using System.Globalization;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperation.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("At", "At", "10/10/2000")] // name size < 3
        [InlineData("Ata", "A", "10/10/2000")] // surname size < 2
        [InlineData("Ata", "At", "10/10/2020")] // age < 18
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname, string dateTime)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel(){
                Name = name, Surname = surname, BirthDate = DateTime.Parse(dateTime, CultureInfo.GetCultureInfo("tr-TR"))
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel(){
                Name = "Ata", Surname = "At", BirthDate = DateTime.Parse("2000/10/10", CultureInfo.GetCultureInfo("tr-TR"))
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().Be(0);
        }
    }
}