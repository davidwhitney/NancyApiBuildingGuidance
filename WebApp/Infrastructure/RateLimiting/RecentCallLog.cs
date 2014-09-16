using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Infrastructure.RateLimiting
{
    public class RecentCallLog
    {
        public string Key { get; set; }
        public List<DateTime> RecentCalls { get; set; }

        public RecentCallLog(string key)
        {
            Key = key;
            RecentCalls = new List<DateTime>();
        }

        public int NumberOfCallsInLast(TimeSpan rateLimitWindow)
        {
            var startOfRateLimitedWindow = EarliestDateTimeForRateLimitWindow(rateLimitWindow);
            return RecentCalls.Count(x => x >= startOfRateLimitedWindow);
        }

        public void RemoveCallsOutsideOf(TimeSpan rateLimitWindow)
        {
            var startOfRateLimitedWindow = EarliestDateTimeForRateLimitWindow(rateLimitWindow);
            RecentCalls.RemoveAll(x => x <= startOfRateLimitedWindow);
        }

        private static DateTime EarliestDateTimeForRateLimitWindow(TimeSpan rateLimitWindow)
        {
            return DateTime.UtcNow.Subtract(rateLimitWindow);
        }

        public void LogCall()
        {
            RecentCalls.Add(DateTime.UtcNow);
        }
    }
}