using Application_Core.Cache;
using Application_Domain;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application_Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            this._cache = cache;
        }

        public async Task<List<T>> GetCachedObject<T>(string cacheKeyPrefix)
        {
            try
            {
                // Construct the key for the cache
                string cacheKey = $"{cacheKeyPrefix}";

                // Get the cached item
                string cachedObjectJson = await this._cache.GetStringAsync(cacheKey);

                // If there was a cached item then deserialise this
                if (!string.IsNullOrEmpty(cachedObjectJson))
                {
                    List<T> cachedObject = JsonConvert.DeserializeObject<List<T>>(cachedObjectJson);
                    return cachedObject;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error in CacheService.CS :- " + ex.Message, ex.InnerException);
            }

            return default(List<T>);
        }

        public async Task<bool> SetCachedObject(string cacheKeyPrefix, dynamic objectToCache)
        {
            string cachedObjectJson = await this._cache.GetStringAsync(cacheKeyPrefix);
            if (!string.IsNullOrEmpty(cachedObjectJson))
            {
                await this.RemoveCache(cacheKeyPrefix);
            }
            try
            {
                string catchdata = JsonConvert.SerializeObject(objectToCache);
                byte[] redisCustomerList = Encoding.UTF8.GetBytes(catchdata);

                string cacheKey = $"{cacheKeyPrefix}";

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(APISetting.CacheConfiguration.AbsoluteExpirationInHours))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(APISetting.CacheConfiguration.SlidingExpirationInMinutes));
                await this._cache.SetAsync(cacheKey, redisCustomerList, options);

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error in CacheService.CS :- " + ex.Message, ex.InnerException);
            }
        }

        //public async Task<bool> CheckExist(string cacheKeyPrefix)
        //{
        //    var redisdata = await _cache.GetStringAsync($"{cacheKeyPrefix}");
        //    if (redisdata != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public async Task<bool> RemoveCache(string cacheKeyPrefix)
        {
            try
            {
                await this._cache.RemoveAsync($"{cacheKeyPrefix}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}