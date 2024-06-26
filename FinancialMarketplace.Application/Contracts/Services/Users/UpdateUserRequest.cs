namespace FinancialMarketplace.Application.Contracts.Services;

public record UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
}