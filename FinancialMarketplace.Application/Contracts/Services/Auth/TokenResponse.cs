namespace FinancialMarketplace.Application.Contracts;

public record TokenResponse
{
    public string Token { get; init; } = string.Empty;
    public int ExpiresIn { get; init; }
    public string RefreshToken { get; init; } = string.Empty;
}