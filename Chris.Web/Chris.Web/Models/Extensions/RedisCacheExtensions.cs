using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Chris.Web.Models.Extensions
{
    public static class RedisCacheExtensions
    {
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T data, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            var jsonData = JsonConvert.SerializeObject(data);

            await cache.SetStringAsync(key, jsonData, options, token);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key, CancellationToken token = default(CancellationToken))
        {
            var jsonData = await cache.GetStringAsync(key, token);

            return jsonData != null ? JsonConvert.DeserializeObject<T>(jsonData) : default(T);
        }
    }
}
