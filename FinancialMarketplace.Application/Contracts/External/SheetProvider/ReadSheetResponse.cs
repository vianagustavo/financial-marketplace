namespace FinancialMarketplace.Application.Contracts.External.Providers.Sheet;

public record ReadSheetResponse<T>
{
    public T? Result { get; init; }

    public string? Error { get; init; }
}