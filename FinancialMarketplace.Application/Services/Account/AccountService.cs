using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Errors;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;
using FinancialMarketplace.Application.Contracts.Database;
using FinancialMarketplace.Domain.Enums;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Users;
using FinancialMarketplace.Domain.Transactions;
using FinancialMarketplace.Application.Contracts.Services.Common;
using System.Collections.ObjectModel;

namespace FinancialMarketplace.Application.Services;

public class AccountService(
    IUnitOfWork unitOfWork,
    IAuthenticatedUserService authenticatedUserService,
    IProductRepository productRepository,
    ITransactionRepository transactionRepository
) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticatedUserService _loggedUser = authenticatedUserService;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

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

    public async Task<ErrorOr<PaginatedResponse<Transaction>>> GetMany(int page, int pageSize, TransactionQueryOptions options)
    {
        if (_loggedUser.User is null || !_loggedUser.User.Role.Permissions.Contains(UserPermissions.NegotiateProducts))
        {
            return ApplicationErrors.User.Permission;
        }

        Transaction[] transactions = await _transactionRepository.GetMany(_loggedUser.User.Account.Id, page, pageSize, options);

        var totalCount = await _transactionRepository.GetCount(options);
        var pageCount = (totalCount + pageSize - 1) / pageSize;

        return new PaginatedResponse<Transaction>
        {
            Items = new Collection<Transaction>(transactions),
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
