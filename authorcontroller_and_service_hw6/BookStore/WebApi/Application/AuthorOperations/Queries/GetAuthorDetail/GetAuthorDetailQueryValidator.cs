using FluentValidation;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0);
        }
    }
}