namespace FinancialMarketplace.Application.Contracts.Common;

public record PaginatedRequest
{
    public virtual int? CurrentPage { get; init; }
    public virtual int? PageSize { get; init; }
}
