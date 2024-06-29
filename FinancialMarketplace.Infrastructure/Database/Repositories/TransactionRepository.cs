using FinancialMarketplace.Application;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Domain.Transactions;

using Microsoft.EntityFrameworkCore;

namespace FinancialMarketplace.Infrastructure.Database.Repositories;

public class TransactionRepository(MyDbContext dbContext) : ITransactionRepository
{
    private readonly MyDbContext _dbContext = dbContext;

    public async Task<Transaction[]> GetMany(Guid accountId, int page, int pageSize, TransactionQueryOptions options)
    {
        Transaction[] transaction = await AppliesFilters(options)
        .Where(t => t.AccountId == accountId)
        .Paginate(page, pageSize)
        .ToArrayAsync();

        return transaction;
    }

    public async Task<int> GetCount(TransactionQueryOptions options)
    {
        return await AppliesFilters(options)
        .CountAsync();
    }

    private IQueryable<Transaction> AppliesFilters(TransactionQueryOptions options)
    {
        return _dbContext.Transactions
            .WhereIf(options.Ids != null, p => options.Ids!.Contains(p.Id))
            .WhereIf(options.Type != null, p => options.Type!.Contains(p.Type))
            .WhereIf(
            options.StartCreatedAt.HasValue,
            p => p.CreatedAt >= options.StartCreatedAt)
            .WhereIf(
            options.EndCreatedAt.HasValue,
            p => p.CreatedAt <= options.EndCreatedAt);
    }
}
