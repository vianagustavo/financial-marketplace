namespace FinancialMarketplace.Application.Database.Repositories;

public record GetUsersQueryOptions
{
    public string? Name { get; init; } = null!;
}