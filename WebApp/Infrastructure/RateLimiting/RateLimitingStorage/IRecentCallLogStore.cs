namespace WebApp.Infrastructure.RateLimiting.RateLimitingStorage
{
    public interface IRecentCallLogStore
    {
        RecentCallLog GetAccessLog(string key);
        void StoreAccessLog(RecentCallLog log);
    }
}