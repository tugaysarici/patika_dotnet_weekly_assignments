using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.AuthorOperation.Queries.GetauthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-4)]
        [InlineData(-10)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldReturnErrors(int authorId)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.Id = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotReturnErrors(int authorId)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.Id = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count().Should().Be(0);
        }
    }
}