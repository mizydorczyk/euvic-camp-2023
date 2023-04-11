using API.Models;
using API.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Sieve.Models;

namespace API.Extensions;

public static class ValidatorsExtension
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
        services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
        services.AddTransient<IValidator<SieveModel>, SieveModelValidator>();

        return services;
    }
}