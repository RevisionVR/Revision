using Revision.Domain.Entities.Users;

namespace Revision.Service.Interfaces.Auth;

public interface ITokenService
{
    string GenerateTokenAsync(User user);
}