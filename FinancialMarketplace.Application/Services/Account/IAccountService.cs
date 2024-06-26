using FinancialMarketplace.Application.Contracts.Services;

using ErrorOr;

namespace FinancialMarketplace.Application.Services;

public interface IAccountService
{
    public Task<ErrorOr<bool>> AddFunds(AddFundsRequest addFundsRequest);
}
