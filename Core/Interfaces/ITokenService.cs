using Core.Entities;

namespace Core.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateTokenAsync(User user);
}