using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetSpace.Identity.Application.Common.Jwt;

public sealed class AccessTokenFactory(IConfiguration configuration)
{
    public AccessTokenResponse GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtAuth:Secret"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["JwtAuth:ValidIssuer"],
            Audience = configuration["JwtAuth:ValidAudience"],
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var accessToken = new AccessTokenResponse(tokenHandler.WriteToken(token));
        return accessToken;
    }

}
