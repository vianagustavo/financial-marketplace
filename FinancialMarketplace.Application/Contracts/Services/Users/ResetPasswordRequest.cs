namespace FinancialMarketplace.Application.Contracts.Services;

public record ResetPasswordRequest
{
    public string Email { get; set; } = null!;
}