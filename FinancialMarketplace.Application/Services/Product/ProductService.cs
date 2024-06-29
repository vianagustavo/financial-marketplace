using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Errors;
using FinancialMarketplace.Domain.Enums;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;
using FinancialMarketplace.Application.Contracts.Services.Common;
using System.Collections.ObjectModel;
using FinancialMarketplace.Application.Contracts.Database;
using FinancialMarketplace.Domain.Products;

namespace FinancialMarketplace.Application.Services;

public class ProductService(
    IUnitOfWork unitOfWork,
    IAuthenticatedUserService authenticatedUserService,
    IProductRepository productRepository
    ) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
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
        {
            return ApplicationErrors.Product.BadRequest;
        }

        Product product = new()
        {
            Name = createProductRequest.Name,
            MinimumValue = createProductRequest.MinimumValue,
            MarketValue = createProductRequest.MarketValue,
            OfferLimitValue = createProductRequest.OfferLimitValue,
            Category = createProductRequest.Category,
            IsActive = createProductRequest.IsActive,
            DueAt = createProductRequest.DueAt,
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

    public async Task<ErrorOr<bool>> Update(Guid productId, UpdateProductRequest updateProductRequest)
    {
        if (_loggedUser.User is null || !_loggedUser.User.Role.Permissions.Contains(UserPermissions.ManageProducts))
        {
            return ApplicationErrors.User.Permission;
        }

        Product? product = await _productRepository.FindById(productId);

        if (product is null)
        {
            return ApplicationErrors.Product.NotFound;
        }

        product.Name = updateProductRequest.Name ?? product.Name;
        product.MinimumValue = updateProductRequest.MinimumValue ?? product.MinimumValue;
        product.MarketValue = updateProductRequest.MarketValue ?? product.MarketValue;
        product.OfferLimitValue = updateProductRequest.OfferLimitValue ?? product.OfferLimitValue;
        product.Category = updateProductRequest.Category ?? product.Category;
        product.IsActive = updateProductRequest.IsActive ?? product.IsActive;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
