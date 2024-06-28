using FinancialMarketplace.Application.Contracts.Services;

using ErrorOr;
using FinancialMarketplace.Domain.Users;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Contracts.Services.Common;

namespace FinancialMarketplace.Application.Services;

public interface IProductService
{
    public Task<ErrorOr<Product>> Add(CreateProductRequest createProductRequest);
    public Task<ErrorOr<PaginatedResponse<Product>>> GetMany(int page, int pageSize, ProductQueryOptions options);
    public Task<ErrorOr<bool>> Update(Guid productId, UpdateProductRequest updateProductRequest);
}
