using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Errors;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Domain.Users;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;
using FinancialMarketplace.Application.Contracts.Services.Common;
using System.Collections.ObjectModel;

namespace FinancialMarketplace.Application.Services;

public class ProductService(
    IAuthenticatedUserService authenticatedUserService,
    IProductRepository productRepository
    ) : IProductService
{
    private readonly IAuthenticatedUserService _loggedUser = authenticatedUserService;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ErrorOr<Product>> Add(CreateProductRequest createProductRequest)
    {
        if (_loggedUser.User is null || !_loggedUser.User.Role.Permissions.Contains(UserPermissions.ManageProducts))
        {
            return ApplicationErrors.User.Permission;
        }

        Product? existingProduct = await _productRepository.FindByName(createProductRequest.Name);

        if (existingProduct is not null)
            return ApplicationErrors.Product.BadRequest;

        Product product = new()
        {
            Name = createProductRequest.Name,
            MinimumValue = createProductRequest.MinimumValue,
            MarketValue = createProductRequest.MarketValue,
            OfferLimitValue = createProductRequest.OfferLimitValue,
            Category = createProductRequest.Category,
            IsActive = createProductRequest.IsActive,
            CreatedBy = _loggedUser.User.Email,
        };

        await _productRepository.Add(product);

        return product;
    }

    public async Task<ErrorOr<PaginatedResponse<Product>>> GetMany(int page, int pageSize, ProductQueryOptions options)
    {
        Product[] products = await _productRepository.GetMany(page, pageSize, options);

        var totalCount = await _productRepository.GetCount(options);
        var pageCount = (totalCount + pageSize - 1) / pageSize;

        return new PaginatedResponse<Product>
        {
            Items = new Collection<Product>(products),
            Pagination = new Pagination
            {
                CurrentPage = page,
                PageCount = pageCount,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }
}
