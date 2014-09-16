using System;
using System.Linq;
using Nancy;
using WebApp.Infrastructure.RateLimiting.RateLimitingStorage;

namespace WebApp.Infrastructure.RateLimiting
{
    public class RateLimitingPipeline
    {
        private const string ApiKeyHeader = "x-api-key";  
        
        private const int RateLimit = 5;
        private static readonly TimeSpan RateLimitWindow = new TimeSpan(0, 0, 0, 10);
        private readonly IRecentCallLogStore _recentCallLogStore;

        public RateLimitingPipeline(IRecentCallLogStore recentCallLogStore)
        {
            _recentCallLogStore = recentCallLogStore;
        }

        public Response RateLimitRequests(NancyContext context)
        {
            if (!context.Request.Headers.Keys.Contains(ApiKeyHeader))
            {
                return HttpStatusCode.Unauthorized;
            }

            var key = context.Request.Headers[ApiKeyHeader].Single();

            if (AccessingApiAtAcceptableRate(key))
            {
                return null;
            }

            return HttpStatusCode.EnhanceYourCalm;
        }

        public bool AccessingApiAtAcceptableRate(string key)
        {
            var log = _recentCallLogStore.GetAccessLog(key);
            var callsInRateLimitedWindow = log.NumberOfCallsInLast(RateLimitWindow);
            var accessAllowed = callsInRateLimitedWindow < RateLimit;

            if (accessAllowed)
            {
                log.LogCall();
            }

            log.RemoveCallsOutsideOf(RateLimitWindow);
            _recentCallLogStore.StoreAccessLog(log);

            return accessAllowed;
        }
    }
}