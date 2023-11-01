using Auto_Gallery.Models;
using FluentValidation;

namespace Auto_Gallery.Validators;

public class AutoValidator : AbstractValidator<Auto>
{
    public AutoValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand field is required.");
        RuleFor(x => x.Model).NotEmpty().WithMessage("Model field is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.ModelYear).GreaterThan(1886).WithMessage("ModelYear must be greater than 1886.");
    }

}