using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeletebookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeletebookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}