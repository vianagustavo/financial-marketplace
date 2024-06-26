using FinancialMarketplace.Application.Contracts;
using FinancialMarketplace.Application.Contracts.Services;

using ErrorOr;

namespace FinancialMarketplace.Application.Services;

public interface IAuthService
{
    Task<ErrorOr<TokenResponse>> Login(LoginUser request);
    Task<ErrorOr<TokenResponse>> Refresh(RefreshTokenRequest request);
}
