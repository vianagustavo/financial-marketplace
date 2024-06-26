namespace FinancialMarketplace.Application.Contracts.Services.Common;

public record Pagination
{
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int PageCount { get; init; }
    public int TotalCount { get; init; }

}

public record PaginatedResponse<T>
{
    public IList<T> Items { get; init; } = [];
    public Pagination Pagination { get; init; } = new();
}
