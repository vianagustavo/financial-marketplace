using FinancialMarketplace.Domain.Enums;

using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Dtos.Product;
public record GetTransactionQueryOptionsDto
{
    [FromQuery(Name = "ids")] public Guid[]? Ids { get; init; }
    [FromQuery(Name = "type")] public TransactionType[]? Type { get; init; }
    [FromQuery(Name = "start_created_at")] public DateTime? StartCreatedAt { get; init; }
    [FromQuery(Name = "end_created_at")] public int? EndCreatedAt { get; init; }
}
