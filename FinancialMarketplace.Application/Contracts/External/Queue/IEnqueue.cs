namespace FinancialMarketplace.Application.Contracts.External.Queue;

public interface IEnqueue
{
    Task Add<T>(T message, string queueName, CancellationToken cancellationToken = default);
    Task<QueueMessageResponse?> GetFirst(string queueName, CancellationToken cancellationToken = default);
    Task Delete(string queueName, string receiptId, CancellationToken cancellationToken = default);
}