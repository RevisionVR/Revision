using Revision.Domain.Entities.Users;

namespace Revision.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateTokenAsync(User userResultDto);
}
