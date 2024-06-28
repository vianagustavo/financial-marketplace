using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Application.Contracts.Services;

public record UpdateProductRequest
{
    public string? Name { get; set; } = null!;
    public decimal? MinimumValue { get; set; }
    public decimal? MarketValue { get; set; }
    public decimal? OfferLimitValue { get; set; }
    public ProductCategory? Category { get; set; }
    public bool? IsActive { get; set; }
}
