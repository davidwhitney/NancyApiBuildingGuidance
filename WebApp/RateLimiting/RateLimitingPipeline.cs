using System;
using Nancy;
using Nancy.TinyIoc;

namespace WebApp.RateLimiting
{
    public class RateLimitingPipeline
    {                
        private readonly IRecentCallLogStore _recentCallLogStore;
        
        private const int RateLimit = 5;
        private static readonly TimeSpan RateLimitWindow = new TimeSpan(0, 0, 0, 10);

        public RateLimitingPipeline(IRecentCallLogStore recentCallLogStore)
        {
            _recentCallLogStore = recentCallLogStore;
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

        public Response RateLimitRequests(NancyContext context, TinyIoCContainer container)
        {
            if (AccessingApiAtAcceptableRate("api-key"))
            {
                return null;
            }

            return HttpStatusCode.EnhanceYourCalm;
        }
    }
}