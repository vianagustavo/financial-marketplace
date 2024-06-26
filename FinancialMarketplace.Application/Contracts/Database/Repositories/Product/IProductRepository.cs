using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Database.Repositories;

public interface IProductRepository
{
    Task<Product> Add(Product product);
    Task<Product?> GetByName(string name);
}
