namespace FinancialMarketplace.Application.Contracts.Services;

public record CreateUserRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}