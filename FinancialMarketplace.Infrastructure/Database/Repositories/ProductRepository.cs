using FinancialMarketplace.Application;
using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Products;

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

    public async Task<Product?> FindByName(string name)
    {
        Product? product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Name == name && p.IsActive == true);

        return product;
    }

    public async Task<Product?> FindById(Guid id)
    {
        Product? product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive == true);

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
        return await AppliesFilters(options)
        .CountAsync();
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
            p => p.MarketValue <= options.MarketValueUpperBound)
            .Where(p => p.IsActive == true);
    }
    public async Task<IList<GroupedExpiringProducts>> GetExpiringProducts()
    {
        DateTime today = DateTime.UtcNow.Date;
        DateTime nextWeek = today.AddDays(7);

        var groupedProducts = await _dbContext.Products
            .Where(p => p.DueAt >= today && p.DueAt <= nextWeek)
            .GroupBy(p => p.CreatedBy)
            .Select(g => new GroupedExpiringProducts
            {
                CreatedBy = g.Key,
                Products = g.ToArray()
            })
            .ToListAsync();

        return groupedProducts;
    }
}
