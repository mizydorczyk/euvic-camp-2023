using API.Helpers;
using API.Middlewares;
using Core.Interfaces;
using Infrastructure.Broadcasting;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using StackExchange.Redis;

namespace API.Extensions;

public static class BroadcastingServiceExtension
{
    public static IServiceCollection AddBroadcastingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BroadcastingDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("BroadcastingConnection") ?? throw new Exception("Broadcasting connection string not found"));
        });

        services.AddSingleton<IConnectionMultiplexer>(x =>
        {
            var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis") ?? throw new Exception("Redis connection string not found"));
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestTimeMiddleware>();
        services.AddScoped<IPieceRepository, PieceRepository>();
        services.AddScoped<IProgrammeItemsRepository, ProgrammeItemsRepository>();
        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
                policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                    .WithOrigins(configuration["CORS:Default"] ?? throw new Exception("CORS origin string not found")));
        });
        return services;
    }
}