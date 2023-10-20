using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-4)]
        [InlineData(-10)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnErrors(int bookId)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnErrors(int bookId)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count().Should().Be(0);
        }
    }
}