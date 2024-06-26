using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Api.Dtos.Users;

public record BasicProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal InitialValue { get; set; }
    public decimal MarketValue { get; set; }
    public ProductCategory Category { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
