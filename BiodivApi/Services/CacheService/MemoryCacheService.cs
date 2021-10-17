using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BiodivApi.Services.CacheService
{
    public class MemoryCacheService: ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly MemoryCacheEntryOptions MemoryCacheEntryOptions = new()
        {
            AbsoluteExpiration = DateTime.Now.AddDays(5),
            Priority = CacheItemPriority.Normal,
            SlidingExpiration = TimeSpan.FromDays(2),
            Size = 1024
        };

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrSet<T>(string key,
            Func<Task<T>> supplier,
            MemoryCacheEntryOptions cacheEntryOptions = null)
        {
            var value = _memoryCache.Get<T>(key);
            if (value != null)
            {
                return value;
            }

            var supplierData = await supplier();
            _memoryCache.Set(key, supplierData, cacheEntryOptions ?? MemoryCacheEntryOptions);
            return supplierData;
        }
    }
}