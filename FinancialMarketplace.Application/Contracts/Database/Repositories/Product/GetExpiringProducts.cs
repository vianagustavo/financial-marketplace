using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Contracts.Database.Repositories;

public class GroupedExpiringProducts
{
    public string CreatedBy { get; set; } = "";
    public Product[] Products { get; set; } = [];
}
