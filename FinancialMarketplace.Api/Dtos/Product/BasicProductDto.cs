using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Api.Dtos.Product;

public record BasicProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal MinimumValue { get; set; }
    public decimal MarketValue { get; set; }
    public decimal OfferLimitValue { get; set; }
    public ProductCategory Category { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
