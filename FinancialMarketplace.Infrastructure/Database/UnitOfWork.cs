using FinancialMarketplace.Application.Contracts.Database;
using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Common.Models;
using FinancialMarketplace.Domain.Users;

namespace FinancialMarketplace.Infrastructure.Database;

public class UnitOfWork
(
    MyDbContext dbContext,
    IUserRepository userRepository,
    IUserTokenRepository userTokenRepository
) : IUnitOfWork
{
    private readonly MyDbContext _dbContext = dbContext;
    private readonly Dictionary<Type, object> _repositories = new(
        [
            new(typeof(User), userRepository),
            new(typeof(UserToken), userTokenRepository)
        ]
    );

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
    {
        var type = typeof(TEntity);

        return _repositories.TryGetValue(type, out object? value)
            ? (IRepository<TEntity>)value
            : throw new InvalidOperationException($"The repository for {type.Name} is not defined.");

    }
}
