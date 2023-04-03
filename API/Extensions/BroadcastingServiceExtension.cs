using Infrastructure.Broadcasting;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions;

public static class BroadcastingServiceExtension
{
    public static IServiceCollection AddBroadcastingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BroadcastingDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("BroadcastingConnection") ?? throw new Exception("Broadcasting connection string not found"));
        });

        services.AddSingleton<IConnectionMultiplexer>(x =>
        {
            var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis") ?? throw new Exception("Redis connection string not found"));
            return ConnectionMultiplexer.Connect(options);
        });

        return services;
    }
}