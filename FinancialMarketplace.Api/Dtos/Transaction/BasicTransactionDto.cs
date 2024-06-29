using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Api.Dtos.Transactions;

public record BasicTransactionDto
{
    public Guid Id { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}
