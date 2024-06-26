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
}
