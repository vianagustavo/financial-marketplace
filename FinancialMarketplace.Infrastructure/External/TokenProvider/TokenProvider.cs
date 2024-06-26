using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using FinancialMarketplace.Application.Contracts.External;
using FinancialMarketplace.Application.Contracts.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinancialMarketplace.Infrastructure.External.TokenProvider;
public class JwtTokenProvider(IConfiguration configuration) : ITokenProvider
{
    private readonly string _secretKey = configuration["API_AUTH_KEY"] ?? throw new ArgumentNullException("API_AUTH_KEY");
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public string Encode(TokenPayload payload, int expiresInSeconds)
    {
        var key = Encoding.UTF8.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("userId", payload.UserId),
                    new Claim("tokenId", payload.TokenId),
                    new Claim(ClaimTypes.Role, "user"),
                }),
            Expires = DateTime.UtcNow.AddSeconds(expiresInSeconds),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    public JwtPayload Decode(string token, bool validateLifetime = true)
    {
        var key = Encoding.UTF8.GetBytes(_secretKey);

        _tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = validateLifetime
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        return jwtToken?.Payload!;

    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
