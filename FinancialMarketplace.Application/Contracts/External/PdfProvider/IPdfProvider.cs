namespace FinancialMarketplace.Application.Contracts.External.Providers.Pdf;

public interface IPdfProvider
{
    Task<MemoryStream> WriteInTable<T>(string title, T[] data);
}
