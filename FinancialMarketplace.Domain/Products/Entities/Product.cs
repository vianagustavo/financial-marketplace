using System.Collections.ObjectModel;

using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Domain.Users;

public class Product : Entity
{
    public required string Name { get; set; } = null!;
    public required decimal InitialValue { get; set; }
    public required decimal MarketValue { get; set; }
    public required ProductCategory Category { get; set; }
    public required bool IsActive { get; set; }
    public required string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Collection<Account> Accounts { get; } = [];

    public Product() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Product(
        Guid id,
        string name,
        decimal initialValue,
        decimal marketValue,
        ProductCategory category,
        bool isActive,
        DateTime updatedAt,
        DateTime deletedAt
    ) : base(id)
    {
        Name = name;
        InitialValue = initialValue;
        MarketValue = marketValue;
        Category = category;
        IsActive = isActive;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }
}
