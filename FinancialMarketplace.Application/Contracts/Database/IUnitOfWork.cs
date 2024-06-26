using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Domain.Common.Models;

namespace FinancialMarketplace.Application.Contracts.Database;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
