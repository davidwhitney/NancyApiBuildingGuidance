using System.Linq;
using Nancy;

namespace WebApp
{
    public static class VersionContaraintExtensions
    {
        private const string VersionHeader = "x-api-version";

        public static bool ForVersion(this NancyContext c, string requiredVersion)
        {
            if (!c.Request.Headers.Keys.Contains(VersionHeader))
            {
                return false;
            }

            return c.Request.Headers[VersionHeader].First() == requiredVersion;
        }
    }
}