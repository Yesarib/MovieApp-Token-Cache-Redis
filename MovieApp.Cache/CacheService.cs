using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Cache
{
    public class CacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCahce)
        {
            _memoryCache = memoryCahce;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T value) ? value : default(T);
        }

        public void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            _memoryCache.Set(key, value, expirationTime);
        }
    }
}
