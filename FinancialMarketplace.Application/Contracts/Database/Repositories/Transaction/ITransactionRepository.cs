using FinancialMarketplace.Domain.Transactions;

namespace FinancialMarketplace.Application.Database.Repositories;

public interface ITransactionRepository
{
    Task<Transaction[]> GetMany(Guid accountId, int page, int pageSize, TransactionQueryOptions options);
    Task<int> GetCount(TransactionQueryOptions options);
}
