using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api
{
    public interface INetworkCache
    {
        CacheResult GetString(string url, TimeSpan? expiry);
        void Save(string url, string result);
        void ClearCache();
    }

    public class CacheResult
    {
        public string Result { get; set; }
        public bool Exists { get; set; }
        public bool Expired { get; set; }
    }

    public class CacheEntry
    {
        public string Key { get; set; }
        public string Data { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastAccessed { get; set; }
    }

    public class NetworkCache : INetworkCache
    {
        private const string TABLE_NAME = "cache";

        private LiteDatabase _db;
        private LiteCollection<CacheEntry> _cache;

        public NetworkCache()
        {
            _db = new LiteDatabase(@"cache.db");
            _cache = (LiteCollection<CacheEntry>)_db.GetCollection<CacheEntry>(TABLE_NAME);
        }

        public void ClearCache()
        {
            if (_cache.Count() > 0)
                _db.DropCollection(TABLE_NAME);

            _cache = (LiteCollection<CacheEntry>)_db.GetCollection<CacheEntry>(TABLE_NAME);
        }

        public CacheResult GetString(string url, TimeSpan? expiry)
        {

            CacheResult result = new CacheResult();

            if (_cache.Count() == 0)
            {
                result.Exists = false;
            }
            else
            {
                // Check to see if there's a matching entry in the DB
                CacheEntry entry = _cache.FindOne(e => e.Key == url);
                if (entry == null)
                {
                    result.Exists = false;
                }
                else
                {
                    // If there is, get the data and set the expiry times.
                    // For this cache, we currently don't have a "last accessed" property on cache entries
                    result.Result = entry.Data;
                    result.Exists = true;
                    if (expiry == null || !expiry.HasValue)
                    {
                        result.Expired = false;
                    }
                    else if (expiry.HasValue)
                    {
                        result.Expired = (DateTime.Now - entry.DateAdded > expiry.Value);
                    }
                }
            }

            return result;
        }

        public void Save(string url, string result)
        {
            if (_cache == null)
                _cache = (LiteCollection<CacheEntry>)_db.GetCollection<CacheEntry>(TABLE_NAME);

            CacheEntry entry = _cache.FindOne(e => e.Key == url);

            if (entry != null)
            {
                entry.Data = result;
                entry.DateLastAccessed = DateTime.Now;

                _cache.Update(entry);
            }
            else
            {
                entry = new CacheEntry()
                {
                    DateLastAccessed = DateTime.Now,
                    DateAdded = DateTime.Now,
                    Data = result,
                    Key = url
                };

                _cache.Insert(entry);
            }
        }
    }
}
