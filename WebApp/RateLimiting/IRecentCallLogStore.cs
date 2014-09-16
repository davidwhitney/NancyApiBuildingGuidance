namespace WebApp.RateLimiting
{
    public interface IRecentCallLogStore
    {
        RecentCallLog GetAccessLog(string key);
        void StoreAccessLog(RecentCallLog log);
    }
}