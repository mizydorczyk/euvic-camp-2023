using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}