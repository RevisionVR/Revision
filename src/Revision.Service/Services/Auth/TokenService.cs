using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Helpers;
using Revision.Service.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Revision.Service.Services.Auth;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateTokenAsync(User user)
    {
        var identityClaims = new Claim[]
        {
            new Claim ("Id", user.Id.ToString()),
            new Claim ("FirstName", user.FirstName),
            new Claim ("LastName", user.LastName),
            new Claim ("Phone", user.Phone),
            new Claim ("Email", user.Email),
            new Claim (ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["23f926fb-dcd2-49f4-8fe2-992aac18f08f"]));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        int expiresHours = 24;

        var token = new JwtSecurityToken(
            issuer: _configuration["http://RevisionVr.uz"],
            audience: _configuration["Revision"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
