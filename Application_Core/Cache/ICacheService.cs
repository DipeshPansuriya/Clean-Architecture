using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application_Core.Cache
{
    public interface ICacheService
    {
        Task<bool> IsConnectedAsync(string key);

        Task<List<T>> GetCachedObject<T>(string cacheKeyPrefix);

        Task<bool> SetCachedObject(string cacheKeyPrefix, dynamic objectToCache);

        //Task<bool> CheckExist(string cacheKeyPrefix);

        Task<bool> RemoveCache(string cacheKeyPrefix);
    }
}