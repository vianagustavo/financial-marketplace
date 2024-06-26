using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Errors;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Domain.Users;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;

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

        Product? existingProduct = await _productRepository.GetByName(createProductRequest.Name);

        if (existingProduct is not null)
            return ApplicationErrors.Product.BadRequest;

        Product product = new()
        {
            Name = createProductRequest.Name,
            InitialValue = createProductRequest.InitialValue,
            MarketValue = createProductRequest.MarketValue,
            Category = createProductRequest.Category,
            IsActive = createProductRequest.IsActive,
            CreatedBy = _loggedUser.User.Email,
        };

        await _productRepository.Add(product);

        return product;
    }
}
