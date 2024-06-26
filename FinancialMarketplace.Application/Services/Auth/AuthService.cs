using System.Globalization;

using Microsoft.Extensions.Configuration;

using FinancialMarketplace.Application.Contracts;
using FinancialMarketplace.Application.Contracts.External;
using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Errors;
using FinancialMarketplace.Infrastructure.External;

using ErrorOr;

namespace FinancialMarketplace.Application.Services;

public class AuthService(
    IConfiguration configuration,
    IUserRepository userRepository,
    ITokenProvider tokenProvider,
    ICryptoHandler cryptoHandler,
    IMemoryService memoryService
) : IAuthService
{
    private readonly int _tokenSecondsExpiration = int.Parse(configuration["TOKEN_SECONDS_EXPIRATION"] ?? throw new ArgumentNullException("TOKEN_SECONDS_EXPIRATION"), NumberStyles.Integer, new CultureInfo("pt-BR"));
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly ICryptoHandler _cryptoHandler = cryptoHandler;
    private readonly IMemoryService _memoryService = memoryService;
    public async Task<ErrorOr<TokenResponse>> Login(LoginUser request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user is null || user.Password is null)
        {
            return ApplicationErrors.AuthErrors.BadRequest;
        }

        var passwordMatch = _cryptoHandler.Compare(request.Password, user.Password!);

        if (!passwordMatch)
            return ApplicationErrors.AuthErrors.BadRequest;

        if (user.DeletedAt != null || user.IsActive == false)
            return ApplicationErrors.User.Invalid;

        var refreshToken = _tokenProvider.GenerateRefreshToken();
        await _memoryService.Set($"{user.Id}-refreshToken", refreshToken);

        TokenPayload payload = new()
        {
            UserId = user.Id.ToString(),
            TokenId = ""
        };

        TokenResponse token = new()
        {
            Token = _tokenProvider.Encode(payload, _tokenSecondsExpiration),
            ExpiresIn = _tokenSecondsExpiration,
            RefreshToken = refreshToken
        };

        return token;
    }

    public async Task<ErrorOr<TokenResponse>> Refresh(RefreshTokenRequest request)
    {
        var payload = _tokenProvider.Decode(request.Token, false);

        var userId = payload["userId"].ToString()!;
        var userRefreshToken = await _memoryService.Get($"{userId}-refreshToken");

        if (userRefreshToken != request.RefreshToken)
            return ApplicationErrors.AuthErrors.BadRefreshTokenRequest;

        var newRefreshToken = _tokenProvider.GenerateRefreshToken();
        await _memoryService.Remove(userId);
        await _memoryService.Set($"{userId}-refreshToken", newRefreshToken);

        TokenPayload tokenPayload = new()
        {
            UserId = userId.ToString(),
            TokenId = ""
        };

        TokenResponse newToken = new()
        {
            Token = _tokenProvider.Encode(tokenPayload, _tokenSecondsExpiration),
            ExpiresIn = _tokenSecondsExpiration,
            RefreshToken = newRefreshToken
        };

        return newToken;
    }
}
