using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Errors;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;
using FinancialMarketplace.Application.Contracts.Database;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Users;
using FinancialMarketplace.Domain.Transactions;

namespace FinancialMarketplace.Application.Services;

public class AccountService(
    IUnitOfWork unitOfWork,
    IAuthenticatedUserService authenticatedUserService,
    IProductRepository productRepository
) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticatedUserService _loggedUser = authenticatedUserService;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ErrorOr<bool>> AddFunds(AddFundsRequest addFundsRequest)
    {
        if (_loggedUser is null || _loggedUser.User is null)
        {
            return ApplicationErrors.AuthErrors.Unauthorized;
        }

        decimal updatedValue = _loggedUser.User.Account.Balance += addFundsRequest.Value;

        _loggedUser.User.Account.Balance = updatedValue;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ErrorOr<bool>> AddProduct(Guid productId)
    {
        if (_loggedUser.User is null || !_loggedUser.User.Role.Permissions.Contains(UserPermissions.NegotiateProducts))
        {
            return ApplicationErrors.User.Permission;
        }

        Product? acquiredProduct = _loggedUser.User.Account.Products.FirstOrDefault(p => p.Id == productId);

        if (acquiredProduct is not null)
        {
            return ApplicationErrors.Product.AcquiredProduct;
        }

        Product? product = await _productRepository.FindById(productId);

        if (product is null)
        {
            return ApplicationErrors.Product.NotFound;
        }

        if (_loggedUser.User.Account.Balance < product.MarketValue)
        {
            return ApplicationErrors.Account.InsufficientFunds;
        }

        Transaction transaction = new()
        {
            Value = product.MarketValue,
            Type = TransactionType.Buy,
            AccountId = _loggedUser.User.Account.Id,
            ProductId = product.Id,
        };

        _loggedUser.User.Account.Products.Add(product);

        _loggedUser.User.Account.Transactions.Add(transaction);

        _loggedUser.User.Account.Balance -= product.MarketValue;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ErrorOr<bool>> SellProduct(Guid productId)
    {
        if (_loggedUser.User is null || !_loggedUser.User.Role.Permissions.Contains(UserPermissions.NegotiateProducts))
        {
            return ApplicationErrors.User.Permission;
        }

        Product? product = _loggedUser.User.Account.Products.FirstOrDefault(p => p.Id == productId);

        if (product is null)
        {
            return ApplicationErrors.Product.NotFound;
        }

        Transaction transaction = new()
        {
            Value = product.MarketValue,
            Type = TransactionType.Sell,
            AccountId = _loggedUser.User.Account.Id,
            ProductId = product.Id,
        };

        _loggedUser.User.Account.Products.Remove(product);

        _loggedUser.User.Account.Transactions.Add(transaction);

        decimal updatedValue = _loggedUser.User.Account.Balance += product.MarketValue;

        _loggedUser.User.Account.Balance = updatedValue;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
