using FluentValidation;
using Sieve.Models;

namespace API.Models.Validators;

public class SieveModelValidator : AbstractValidator<SieveModel>
{
    public SieveModelValidator()
    {
        RuleFor(x => x.PageSize).LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
    }
}