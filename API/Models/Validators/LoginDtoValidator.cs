using FluentValidation;

namespace API.Models.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be empty.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password must not be empty.");
    }
}