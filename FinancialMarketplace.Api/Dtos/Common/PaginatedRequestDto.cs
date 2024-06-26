
using FinancialMarketplace.Application.Contracts.Common;

using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Dtos.Common;

public record PaginatedRequestDto : PaginatedRequest
{
    [FromQuery(Name = "current_page")]
    public new int? CurrentPage { get; init; }

    [FromQuery(Name = "page_size")]
    public new int? PageSize { get; init; }
}
