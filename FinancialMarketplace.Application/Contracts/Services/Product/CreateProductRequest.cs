using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Application.Contracts.Services;

public record CreateProductRequest
{
    public string Name { get; set; } = null!;
    public decimal InitialValue { get; set; }
    public decimal MarketValue { get; set; }
    public ProductCategory Category { get; set; }
    public bool IsActive { get; set; }
}
