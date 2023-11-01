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
    private IConfiguration _config;

    public TokenService(IConfiguration configuration)
    {
        _config = configuration;
    }
    public string GenerateTokenAsync(User userResultDto)
    {
        var identityClaims = new Claim[]
        {
            new Claim ("Id", userResultDto.Id.ToString()),
            new Claim ("FirstName", userResultDto.FirstName),
            new Claim ("LastName", userResultDto.LastName),
            new Claim ("Phone", userResultDto.Phone),
            new Claim ("Email", userResultDto.Email),
            new Claim (ClaimTypes.Role, userResultDto.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        int expiresHours = 24;

        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
