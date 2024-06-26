using System.Linq.Expressions;

using FinancialMarketplace.Domain.Common.Models;

namespace FinancialMarketplace.Application.Contracts.Database.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task Add(TEntity entity);
    Task<TEntity[]> AddMany(TEntity[] entities);
    void Update(TEntity entity);
    Task<TEntity[]> UpdateMany(TEntity[] entities);
    void Delete(TEntity entity);
    Task<TEntity?> FindById(Guid id, string[]? includes = null);
    Task<IList<TEntity>> FindWhere<TOrderKey>
    (
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TOrderKey>> orderBy,
        string[]? includes = null
    );

    Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity[]> FindMany(Expression<Func<TEntity, bool>> predicate);
    Task<T> ExecuteInTransaction<T>(Func<Task<T>> func);
    Task ExecuteInTransaction(Func<Task> func);
}
