using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Services.Auth;

public interface IAuthenticatedUserService
{
    public User? User { get; set; }
}
