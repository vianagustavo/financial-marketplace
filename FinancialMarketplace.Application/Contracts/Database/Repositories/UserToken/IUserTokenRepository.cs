using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Application.Database.Repositories;

public interface IUserTokenRepository
{
    Task<UserToken> Add(UserToken userToken);
    Task<UserToken?> GetUserActiveToken(Guid userId);
    Task UpdateUserActiveTokens(Guid userId);
    Task<UserToken> UpdateUsedToken(UserToken userToken);
}
