using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BiodivApi.Services.CacheService
{
    public interface ICacheService
    {
        Task<T> GetOrSet<T>(string key, Func<Task<T>> supplier,MemoryCacheEntryOptions cacheEntryOptions = null);
    }
}