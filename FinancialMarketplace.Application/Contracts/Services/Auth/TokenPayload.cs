namespace FinancialMarketplace.Application.Contracts.Services;

public record TokenPayload
{
    public string UserId { get; set; } = null!;
    public string TokenId { get; set; } = null!;
}