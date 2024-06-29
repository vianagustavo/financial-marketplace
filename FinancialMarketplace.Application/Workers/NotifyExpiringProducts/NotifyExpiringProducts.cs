using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Application.Database.Repositories;

namespace FinancialMarketplace.Application.Services.Workers;

public class NotifyExpiringProductsWorker(
    IProductRepository productRepository
) : INotifyExpiringProducts
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task NotifyExpiringProducts()
    {
        Console.WriteLine("chamouu");

        IList<GroupedExpiringProducts> groupedProducts = await _productRepository.GetExpiringProducts();

        foreach (GroupedExpiringProducts group in groupedProducts)
        {
            Console.WriteLine($"Email: {group.CreatedBy}");
            foreach (var product in group.Products)
            {
                Console.WriteLine($"  Product: {product.Name}, DueAt: {product.DueAt}");
            }
        }
    }

}
