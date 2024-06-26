namespace FinancialMarketplace.Application.Contracts.Services;

public record CreatePasswordRequest
{
    public string Token { get; set; } = null!;
    public string Password { get; set; } = null!;
}