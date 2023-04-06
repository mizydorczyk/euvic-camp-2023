using Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty.")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty.")
            .Must(x => x.Any(char.IsUpper)).WithMessage("Password must consist of at least one uppercase.")
            .Must(x => x.Any(char.IsLower)).WithMessage("Password must consist of at least one lowercase.")
            .Must(x => x.Any(char.IsDigit)).WithMessage("Password must consist of at least one digit.")
            .MinimumLength(8).WithMessage("Password must consist of at least 8 characters.");
    }
}