using System.Globalization;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperation.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Te", "Te", "10/10/2000")]
        [InlineData("Tes", "T", "10/10/2000")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname, string dateTime)
        {
            var model = new UpdateAuthorModel() { Name = name, Surname = surname, BirthDate = DateTime.Parse(dateTime, CultureInfo.GetCultureInfo("tr-TR")) };
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = model;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            var model = new UpdateAuthorModel() { Name = "Test", Surname = "Test", BirthDate = DateTime.Parse("01/01/1998", CultureInfo.GetCultureInfo("tr-TR")) };
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = model;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}