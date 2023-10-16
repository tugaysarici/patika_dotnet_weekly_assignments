using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname.Trim() != string.Empty);
            RuleFor(command => command.Model.BirthDate).GreaterThan(DateTime.MinValue);
        }
    }
}