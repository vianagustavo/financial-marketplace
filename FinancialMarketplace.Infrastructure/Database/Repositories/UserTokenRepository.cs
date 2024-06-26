using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace FinancialMarketplace.Infrastructure.Database.Repositories;

public class UserTokenRepository(MyDbContext dbContext) : IUserTokenRepository
{
    private readonly MyDbContext _dbContext = dbContext;

    public async Task<UserToken> Add(UserToken userToken)
    {
        await _dbContext.AddAsync(userToken);

        await _dbContext.SaveChangesAsync();

        return userToken;
    }

    public async Task<UserToken?> GetUserActiveToken(Guid userId)
    {
        var userTokens = await _dbContext.UserTokens
            .FirstOrDefaultAsync(userTokens => userTokens.Id == userId && userTokens.IsUsed == false);

        return userTokens;
    }

    public async Task UpdateUserActiveTokens(Guid userId)
    {
        _dbContext.UserTokens
            .Where(userToken => userToken.UserId == userId && userToken.IsUsed == false)
            .ExecuteUpdate(userToken => userToken
            .SetProperty(t => t.IsUsed, true)
            .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));

        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserToken> UpdateUsedToken(UserToken userToken)
    {
        userToken.IsUsed = true;
        userToken.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return userToken;
    }
}
