using FinancialMarketplace.Domain.Accounts;
using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Domain.Products;

namespace FinancialMarketplace.Domain.Transactions;

public class Transaction : Entity
{
    public required decimal Value { get; set; }
    public required TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public required Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Transaction() : base(Guid.NewGuid())
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Transaction(
        Guid id,
        decimal value,
        TransactionType type,
        DateTime updatedAt,
        DateTime deletedAt,
        Guid accountId,
        Account account,
        Guid productId,
        Product product
    ) : base(id)
    {
        Value = value;
        Type = type;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
        AccountId = accountId;
        Account = account;
        ProductId = productId;
        Product = product;
    }
}
