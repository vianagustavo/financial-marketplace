namespace FinancialMarketplace.Application.Contracts.External.Queue;

public record QueueMessageResponse
{
    public required string Id { get; init; }

    public required string Message { get; init; }
    public required string ReceiptId { get; init; }
}