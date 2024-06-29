using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Application.Contracts.External;
using FinancialMarketplace.Application.Contracts.Tools;
using FinancialMarketplace.Application.Database.Repositories;

namespace FinancialMarketplace.Application.Services.Workers;

public class NotifyExpiringProductsWorker(
    IProductRepository productRepository,
    IEmailProvider emailProvider,
    IEmailTemplateBuilder emailTemplateBuilder
) : INotifyExpiringProducts
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IEmailProvider _emailProvider = emailProvider;
    private readonly IEmailTemplateBuilder _emailTemplateBuilder = emailTemplateBuilder;

    public async Task NotifyExpiringProducts()
    {
        IList<GroupedExpiringProducts> groupedProducts = await _productRepository.GetExpiringProducts();

        foreach (GroupedExpiringProducts group in groupedProducts)
        {
            await _emailProvider.Send(group.CreatedBy, "Report de produtos", _emailTemplateBuilder.ExpiringProductsTemplate(group.Products));
        }
    }

}
