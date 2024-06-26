using System.Linq.Expressions;

using FinancialMarketplace.Application.Contracts.Database.Repositories;
using FinancialMarketplace.Domain.Common.Models;

using Microsoft.EntityFrameworkCore;

namespace FinancialMarketplace.Infrastructure.Database.Repositories;

public class Repository<TEntity>
(
    MyDbContext dbContext,
    DapperConnection dapperConnection
) : IRepository<TEntity> where TEntity : Entity
{
    protected internal MyDbContext DbContext { get; } = dbContext;
    protected internal DapperConnection Dapper { get; } = dapperConnection;
    protected internal DbSet<TEntity> DbSet { get; } = dbContext.Set<TEntity>();

    public async Task Add(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task<TEntity[]> AddMany(TEntity[] entities)
    {
        await DbSet.AddRangeAsync(entities);

        await DbContext.SaveChangesAsync();

        return entities;
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task<TEntity[]> UpdateMany(TEntity[] entities)
    {
        DbSet.UpdateRange(entities);

        await DbContext.SaveChangesAsync();

        return entities;
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<TEntity?> FindById(Guid id, string[]? includes = null)
    {
        if (includes != null && includes.Length > 0)
        {
            var query = DbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        return await DbSet.FindAsync(id);
    }

    public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsQueryable().Where(predicate).FirstOrDefaultAsync() ?? null;
    }

    public async Task<TEntity[]> FindMany(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsQueryable().Where(predicate).ToArrayAsync();
    }

    public async Task<T> ExecuteInTransaction<T>(Func<Task<T>> func)
    {
        var numberOfRetries = 0;
        var lastException = null as Exception;

        while (true)
        {
            if (numberOfRetries > 10)
            {
                throw lastException!;
            }

            var executor = DbContext.Database.CreateExecutionStrategy();

            try
            {
                return await executor.ExecuteAsync(async () =>
                {
                    await using var transaction = await DbContext.Database.BeginTransactionAsync();
                    var result = await func();
                    await transaction.CommitAsync();
                    return result;
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                lastException = ex;
                numberOfRetries++;
            }
        }
    }

    public async Task ExecuteInTransaction(Func<Task> func)
    {
        await ExecuteInTransaction(async () =>
        {
            await func();
            return 0;
        });
    }

    public virtual async Task<IList<TEntity>> FindWhere<TOrderKey>
    (
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TOrderKey>> orderBy,
        string[]? includes = null
    )
    {
        var query = DbSet.AsQueryable();

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await DbSet.Where(predicate).OrderBy(orderBy).ToListAsync();
    }
}
