using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace Ez.Web.Infrastructure
{
    public class MemoryCacher
    {
        //Thank you http://blog.developers.ba/simple-way-implement-caching-asp-net-web-api/
        public object GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public bool Upsert(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                //if the value from the cache changed,  replace it
                if (value.Equals(memoryCache.Contains(key)))
                {
                    return false;
                }
                memoryCache.Remove(key);
            }
            return memoryCache.Add(key, value, absExpiration);
        }

        public void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }
        public bool Exists(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Contains(key);
        }
    }
}