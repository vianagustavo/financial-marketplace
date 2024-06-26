using FinancialMarketplace.Application.Contracts.Services;

using ErrorOr;
using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Services;

public interface IProductService
{
    public Task<ErrorOr<Product>> Add(CreateProductRequest createProductRequest);
}
