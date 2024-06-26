using FinancialMarketplace.Application.Contracts.Services;
using FinancialMarketplace.Application.Errors;

using ErrorOr;
using FinancialMarketplace.Application.Services.Auth;
using FinancialMarketplace.Application.Contracts.Database;

namespace FinancialMarketplace.Application.Services;

public class AccountService(
    IUnitOfWork unitOfWork,
    IAuthenticatedUserService authenticatedUserService
) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticatedUserService _loggedUser = authenticatedUserService;

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
}
