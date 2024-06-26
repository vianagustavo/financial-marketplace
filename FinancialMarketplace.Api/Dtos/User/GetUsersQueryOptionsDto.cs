using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Dtos.Users;
public record GetUsersQueryOptionsDto
{
    [FromQuery(Name = "name")] public string? Name { get; init; } = null!;
}