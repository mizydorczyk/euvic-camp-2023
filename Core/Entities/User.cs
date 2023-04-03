using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}