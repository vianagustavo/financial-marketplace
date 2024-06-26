namespace FinancialMarketplace.Application.Contracts.Services;

public record RefreshTokenRequest
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}