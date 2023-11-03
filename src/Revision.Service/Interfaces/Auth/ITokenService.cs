using Revision.Domain.Entities.Users;

namespace Revision.Service.Interfaces.Auth;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
}