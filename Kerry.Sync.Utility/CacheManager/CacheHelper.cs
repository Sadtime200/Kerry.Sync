using System.Collections;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web.Hosting;

namespace Kerry.Sync.Utility
{
    public class CacheHelper
    {
        private static Cache _cache;

        public static double SaveTime { get; set; }

        static CacheHelper()
        {
            _cache = HostingEnvironment.Cache;
            SaveTime = 30.0;
        }

        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            return _cache.Get(key);
        }


        /// <summary>
        /// Attempt to get cache. if exits, return true.and set the cache value to param 'outValue'. 
        /// </summary>
        /// <typeparam name="T">Cache type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="outValue">Cache value</param>
        /// <returns></returns>
        public static bool TryGetValue<T>(string key, out T outValue)
        {
            outValue = Get<T>(key) == null ? default(T) : Get<T>(key);
            return outValue != null;
        }



        /// <summary>
        /// Try to get cache according to cache key 
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="key">cache key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return _cache.Get(key) == null ? default(T) : (T)_cache.Get(key);
        }


        /// <summary>
        /// isnert item into cache
        /// </summary>
        /// <typeparam name="T">cache type</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">cache value</param>
        public static void Set<T>(string key, T value)
        {
            _cache.Insert(key, value);
        }

        /// <summary>
        /// isnert item into cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependency"></param>
        /// <param name="priority"></param>
        /// <param name="callback"></param>
        //public static void Set<T>(string key, T value, CacheDependency dependency, CacheItemPriority priority, CacheItemRemovedCallback callback)
        //{
        //    _cache.Insert(key, value, dependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(SaveTime), priority, callback);
        //}
        public static void Set<T>(string key, T value, CacheDependency dependency, CacheItemPriority priority, CacheItemRemovedCallback callback)
        {
            _cache.Insert(key, value, dependency, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, callback);
        }
        /// <summary>
        /// isnert item into cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependency"></param>
        /// <param name="callback"></param>
        public static void Set<T>(string key, T value, CacheDependency dependency, CacheItemRemovedCallback callback)
        {
            Set<T>(key, value, dependency, CacheItemPriority.Default, callback);
        }
        /// <summary>
        /// isnert item into cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependency"></param>
        public static void Set<T>(string key, T value, CacheDependency dependency)
        {
            Set<T>(key, value, dependency, CacheItemPriority.Default, null);
        }


        /// <summary>
        /// Get all cache keys.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAllKeys()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Key.ToString();
            }
        }

        /// <summary>
        /// Remove specified cache item.
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            _cache.Remove(key);
        }

        /// <summary>
        /// Remove all cache item
        /// </summary>
        public static void RemoveAll()
        {
            IEnumerable<string> keys = GetAllKeys();
            foreach (string k in keys)
            {
                _cache.Remove(k);
            }

        }

        /// <summary>
        /// Check cache item via cache key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            return _cache.Get(key) == null ? false : true;
        }


        ///// <summary>
        ///// Get absolute expiration time
        ///// </summary>
        ///// <param name="cacheTime">cache exipire time</param>
        ///// <param name="cacheExpiration">expiration type</param>
        ///// <returns></returns>
        //public static DateTime GetAbsoluteExpirationTime(CacheTime cacheTime, CacheExpiration cacheExpiration);

        ///// <summary>
        ///// clear specified cache.
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="modifydenpend"></param>
        //public static void RemoveAt(string key, bool modifydenpend);


        ///// <summary>
        ///// Add cache
        ///// </summary>
        ///// <typeparam name="T">Cache type</typeparam>
        ///// <param name="key">Key</param>
        ///// <param name="value">Cache value</param>
        ///// <param name="cacheTime">Cache expired time</param>
        //public static void Set<T>(string key, T value, CacheTime cacheTime);



    }


    /// <summary>
    /// Cache time setting
    /// </summary>
    public enum CacheTime
    {
        // Summary:
        //     one min
        OneMinute = 1,
        //
        // Summary:
        //     short time
        Short = 3,
        //
        // Summary:
        //     normal expired time
        Normal = 10,
        //
        // Summary:
        //     long time
        Long = 60,
        Long_ThreeHour = 180,
        //
        // Summary:
        //     never expired
        NotRemovable = 181,
        //
        // Summary:
        //     ten seconds
        TenSecond = 182,
        ThirstSecond = 183,
    }

    /// <summary>
    /// Expiration type
    /// </summary>
    public enum CacheExpiration
    {
        // Summary:
        //     Absolute expiration
        AbsoluteExpiration = 0,
        //
        // Summary:
        //     Sliding  expiration
        SlidingExpiration = 1,
    }
}
