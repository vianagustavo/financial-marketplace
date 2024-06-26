namespace FinancialMarketplace.Application.Contracts.Services;

public record LoginUser
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}