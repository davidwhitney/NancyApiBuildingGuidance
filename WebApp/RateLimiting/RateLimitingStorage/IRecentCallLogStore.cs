namespace WebApp.RateLimiting.RateLimitingStorage
{
    public interface IRecentCallLogStore
    {
        RecentCallLog GetAccessLog(string key);
        void StoreAccessLog(RecentCallLog log);
    }
}