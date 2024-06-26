using Microsoft.Extensions.Caching.Memory;

namespace FinancialMarketplace.Infrastructure.External;

public class MemoryCacheService(IMemoryCache memoryCache) : IMemoryService
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task Set(string key, string obj)
    {
        _memoryCache.Set(key, obj);
        await Task.CompletedTask;
    }


    public async Task<string?> Get(string key)
    {
        return await Task.FromResult(_memoryCache.Get<string?>(key));
    }

    public async Task Remove(string key)
    {
        _memoryCache.Remove(key);
        await Task.CompletedTask;
    }
}
