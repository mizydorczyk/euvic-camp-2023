using System.Text;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("IdentityConnection") ?? throw new Exception("Identity connection string not found"));
        });

        services.AddIdentity<User, Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<User>>("EmailConfirmation");

        services.Configure<IdentityOptions>(options => { options.Password.RequireNonAlphanumeric = false; });
        services.Configure<IdentityOptions>(options => { options.SignIn.RequireConfirmedEmail = true; });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Token:Issuer"] ?? throw new Exception("Issuer is not specified"),
                ValidAudience = configuration["Token:Audience"] ?? throw new Exception("Audience is not specified"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"] ?? throw new Exception("Key is not specified"))),
            };
        });
        
        services.AddAuthorization();

        return services;
    }
}