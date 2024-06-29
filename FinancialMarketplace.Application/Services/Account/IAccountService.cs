using FinancialMarketplace.Application.Contracts.Services;

using ErrorOr;
using FinancialMarketplace.Application.Contracts.Services.Common;
using FinancialMarketplace.Domain.Transactions;
using FinancialMarketplace.Application.Database.Repositories;

namespace FinancialMarketplace.Application.Services;

public interface IAccountService
{
    public Task<ErrorOr<bool>> AddFunds(AddFundsRequest addFundsRequest);
    public Task<ErrorOr<bool>> AddProduct(Guid productId);
    public Task<ErrorOr<bool>> SellProduct(Guid productId);
    public Task<ErrorOr<PaginatedResponse<Transaction>>> GetMany(int page, int pageSize, TransactionQueryOptions options);
}
