using System.Collections.ObjectModel;

using FinancialMarketplace.Domain.Accounts;
using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Domain.Transactions;

namespace FinancialMarketplace.Domain.Products;

public class Product : Entity
{
    public required string Name { get; set; } = null!;
    public required decimal MinimumValue { get; set; }
    public required decimal MarketValue { get; set; }
    public required decimal OfferLimitValue { get; set; }
    public required ProductCategory Category { get; set; }
    public required bool IsActive { get; set; }
    public required string CreatedBy { get; set; } = null!;
    public DateTime DueAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Collection<Account> Accounts { get; } = [];
    public Collection<Transaction> Transactions { get; } = [];

    public Product() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Product(
        Guid id,
        string name,
        decimal minimumValue,
        decimal marketValue,
        decimal offerLimitValue,
        ProductCategory category,
        bool isActive,
        DateTime dueAt,
        DateTime updatedAt,
        DateTime deletedAt
    ) : base(id)
    {
        Name = name;
        MinimumValue = minimumValue;
        MarketValue = marketValue;
        OfferLimitValue = offerLimitValue;
        Category = category;
        IsActive = isActive;
        DueAt = dueAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }
}
