using System.Text;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services;
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
            .AddEntityFrameworkStores<IdentityDbContext>();

        services.Configure<IdentityOptions>(options => { options.Password.RequireNonAlphanumeric = false; });

        services.Configure<IdentityOptions>(options =>
        {
            // options.SignIn.RequireConfirmedEmail = true;
        });

        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"] ?? throw new Exception("Key is not specified"))),
                    ValidIssuer = configuration["Token:Issuer"] ?? throw new Exception("Issuer is not specified"),
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        services.AddAuthorization();

        return services;
    }
}