using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Database.Repositories;

public interface IProductRepository
{
    Task<Product> Add(Product product);
    Task<Product?> FindByName(string name);
    Task<Product?> FindById(Guid id);
    Task<Product[]> GetMany(int page, int pageSize, ProductQueryOptions options);
    Task<int> GetCount(ProductQueryOptions options);
}
