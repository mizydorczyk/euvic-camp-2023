using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        services.Configure<IdentityOptions>(options => { options.Password.RequireNonAlphanumeric = true; });
        services.Configure<IdentityOptions>(options => { options.SignIn.RequireConfirmedEmail = true; });

        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Cookies";
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "Cookies";
        }).AddCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200) ctx.Response.StatusCode = 401;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200) ctx.Response.StatusCode = 403;
                    return Task.CompletedTask;
                }
            };
            options.ExpireTimeSpan = TimeSpan.FromDays(3);
        });

        services.AddAuthorization();

        return services;
    }
}