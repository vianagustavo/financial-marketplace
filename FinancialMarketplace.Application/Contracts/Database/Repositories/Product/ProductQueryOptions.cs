using FinancialMarketplace.Domain.Enums;

namespace FinancialMarketplace.Application.Database.Repositories;

public record ProductQueryOptions
{
    public Guid[]? Ids { get; init; }
    public ProductCategory[]? Category { get; init; }
    public decimal? MarketValueLowerBound { get; init; }
    public decimal? MarketValueUpperBound { get; init; }
    public string? CreatedBy { get; init; }

    public override string ToString()
    {
        return $"Ids: {string.Join(", ", Ids ?? Array.Empty<Guid>())}\n" +
            $"Category: {string.Join(", ", Category?.Select(c => c.ToString()) ?? Array.Empty<string>())}\n" +
            $"MarketValueLowerBound: {MarketValueLowerBound}\n" +
            $"MarketValueUpperBound: {MarketValueUpperBound}\n" +
            $"CreatedBy: {CreatedBy}\n";
    }
}
