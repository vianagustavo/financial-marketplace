using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Services.Auth;
public class AuthenticatedUserService : IAuthenticatedUserService
{
    public User? User { get; set; }
}
