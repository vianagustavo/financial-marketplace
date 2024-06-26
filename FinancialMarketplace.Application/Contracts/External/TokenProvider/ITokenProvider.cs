using System.IdentityModel.Tokens.Jwt;

using FinancialMarketplace.Application.Contracts.Services;

namespace FinancialMarketplace.Application.Contracts.External;

public interface ITokenProvider
{
    string Encode(TokenPayload payload, int expiresInSeconds);
    JwtPayload Decode(string token, bool validateLifetime = true);
    string GenerateRefreshToken();
}
