using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.BirthDate).NotEmpty().Must(BeOver18YearsOld).WithMessage("Author must be over than 18 years old.");
        }

        private bool BeOver18YearsOld(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age))
            {
                age--;
            }
            return age >= 18;
        }
    }
}