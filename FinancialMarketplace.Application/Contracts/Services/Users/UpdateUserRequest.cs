namespace FinancialMarketplace.Application.Contracts.Services;

public record UpdateUserRoleRequest
{
    public Guid RoleId { get; set; }
}
