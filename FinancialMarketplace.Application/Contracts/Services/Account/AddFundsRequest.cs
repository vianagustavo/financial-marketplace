namespace FinancialMarketplace.Application.Contracts.Services;

public record AddFundsRequest
{
    public decimal Value { get; set; }
}
