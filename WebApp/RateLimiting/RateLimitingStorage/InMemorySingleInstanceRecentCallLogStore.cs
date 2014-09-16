using System.Collections.Concurrent;

namespace WebApp.RateLimiting.RateLimitingStorage
{
    public class InMemorySingleInstanceRecentCallLogStore : IRecentCallLogStore
    {
        private readonly ConcurrentDictionary<string, RecentCallLog> _storage;

        public InMemorySingleInstanceRecentCallLogStore()
        {
            _storage = new ConcurrentDictionary<string, RecentCallLog>();
        }

        public RecentCallLog GetAccessLog(string key)
        {
            return _storage.GetOrAdd(key, s => new RecentCallLog(key));
        }

        public void StoreAccessLog(RecentCallLog log)
        {
            // In memory, storing isn't required. Obviously totally inappropriate for a web farm.
        }
    }
}