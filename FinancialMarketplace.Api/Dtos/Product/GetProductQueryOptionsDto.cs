using FinancialMarketplace.Domain.Enums;

using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Dtos.Product;
public record GetProductsQueryOptionsDto
{
    [FromQuery(Name = "ids")] public Guid[]? Ids { get; init; }
    [FromQuery(Name = "category")] public ProductCategory[]? Category { get; init; }
    [FromQuery(Name = "market_value_lower_bound")] public int? MarketValueLowerBound { get; init; }
    [FromQuery(Name = "market_value_upper_bound")] public int? MarketValueUpperBound { get; init; }
    [FromQuery(Name = "created_by")] public string? CreatedBy { get; init; }
}
