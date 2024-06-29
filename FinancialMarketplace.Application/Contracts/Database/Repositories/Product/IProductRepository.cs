using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Domain.Products;

namespace FinancialMarketplace.Application.Database.Repositories;

public interface IProductRepository
{
    Task<Product> Add(Product product);
    Task<Product?> FindByName(string name);
    Task<Product?> FindById(Guid id);
    Task<Product[]> GetMany(int page, int pageSize, ProductQueryOptions options);
    Task<int> GetCount(ProductQueryOptions options);
    Task<IList<GroupedExpiringProducts>> GetExpiringProducts();
}
