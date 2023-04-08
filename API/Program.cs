using API.Extensions;
using API.Middlewares;
using Core.Entities;
using Infrastructure.Broadcasting;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddBroadcastingServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddFluentValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseSwaggerDocumentation();

app.UseCors("Default");

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var broadcastingDbContext = services.GetRequiredService<BroadcastingDbContext>();
    var identityDbContext = services.GetRequiredService<IdentityDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();

    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        await broadcastingDbContext.Database.MigrateAsync();
        await identityDbContext.Database.MigrateAsync();
        await BroadcastingDbContextSeed.SeedAsync(broadcastingDbContext);
        await IdentityDbContextSeed.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occured during migration");
    }
}

app.Run();