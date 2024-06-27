using FinancialMarketplace.Application;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace FinancialMarketplace.Infrastructure.Database.Repositories;

public class ProductRepository(MyDbContext dbContext) : IProductRepository
{
    private readonly MyDbContext _dbContext = dbContext;

    public async Task<Product> Add(Product product)
    {
        await _dbContext.AddAsync(product);

        await _dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> GetByName(string name)
    {
        Product? product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Name == name && p.IsActive == true);

        return product;
    }

    public async Task<Product[]> GetMany(int page, int pageSize, ProductQueryOptions options)
    {
        Product[] product = await AppliesFilters(options)
        .Paginate(page, pageSize)
        .ToArrayAsync();

        return product;
    }

    public async Task<int> GetCount(ProductQueryOptions options)
    {
        return await AppliesFilters(options).CountAsync();
    }

    private IQueryable<Product> AppliesFilters(ProductQueryOptions options)
    {
        return _dbContext.Products
            .WhereIf(options.Ids != null, p => options.Ids!.Contains(p.Id))
            .WhereIf(options.CreatedBy != null, p => options.CreatedBy!.Contains(options.CreatedBy))
            .WhereIf(options.Category != null, p => options.Category!.Contains(p.Category))
            .WhereIf(
            options.MarketValueLowerBound.HasValue,
            p => p.MarketValue >= options.MarketValueLowerBound)
            .WhereIf(
            options.MarketValueUpperBound.HasValue,
            p => p.MarketValue <= options.MarketValueUpperBound);
    }
}
