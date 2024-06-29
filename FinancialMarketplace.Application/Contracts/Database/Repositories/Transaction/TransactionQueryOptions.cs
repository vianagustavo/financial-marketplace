using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Application.Database.Repositories;

public record TransactionQueryOptions
{
    public Guid[]? Ids { get; init; }
    public TransactionType[]? Type { get; init; }
    public DateTime? StartCreatedAt { get; init; }
    public DateTime? EndCreatedAt { get; init; }

    public override string ToString()
    {
        return $"Ids: {string.Join(", ", Ids ?? Array.Empty<Guid>())}\n" +
            $"Type: {string.Join(", ", Type?.Select(c => c.ToString()) ?? Array.Empty<string>())}\n" +
            $"StartCreatedAt: {StartCreatedAt}\n" +
            $"EndCreatedAt: {EndCreatedAt}\n";
    }
}
