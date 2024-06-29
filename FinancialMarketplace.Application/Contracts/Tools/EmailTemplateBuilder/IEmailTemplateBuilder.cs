using FinancialMarketplace.Domain.Products;

namespace FinancialMarketplace.Application.Contracts.Tools;

public interface IEmailTemplateBuilder
{
    string DefinePasswordTemplate(string token, string username);
    string ResetPasswordTemplate(string token, string username);
    string ExpiringProductsTemplate(Product[] products);
}
