using System.Security.Claims;
using Core.Entities;

namespace Core.Interfaces;

public interface ICookieService
{
    public Task<ClaimsIdentity> GetClaimsIdentity(User user);
}