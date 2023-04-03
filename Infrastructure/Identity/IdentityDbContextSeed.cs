using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public static class IdentityDbContextSeed
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<Role>
        {
            new() { Name = "User" },
            new() { Name = "Admin" }
        };

        foreach (var role in roles) await roleManager.CreateAsync(role);

        var admin = new User
        {
            UserName = "admin",
            Email = "admin@test.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRoleAsync(admin, "Admin");

        var user = new User
        {
            UserName = "user",
            Email = "user@test.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "Pa$$w0rd");
        await userManager.AddToRoleAsync(user, "User");
    }
}