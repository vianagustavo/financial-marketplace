namespace FinancialMarketplace.Infrastructure.External;

public interface IMemoryService
{
    Task Set(string key, string obj);
    Task<string?> Get(string key);
    Task Remove(string key);
}