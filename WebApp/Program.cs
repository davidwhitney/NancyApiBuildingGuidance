using System;
using System.Linq;
using Nancy;
using Nancy.Hosting.Self;

namespace WebApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = new NancyHost(new Uri("http://localhost:1337"));
            host.Start();
            
            Console.WriteLine("Press ANY key to exit");
            Console.ReadLine();
        }
    }

    public class VersionedApi : NancyModule
    {
        public VersionedApi()
            : base("/versioned-api")
        {
            Get["/"] = param => Response.AsJson(new { hello = "world v-unknown" });
            Get["/", c => c.ForVersion("1")] = param => Response.AsJson(new { hello = "world v1" });
            Get["/", c => c.ForVersion("2")] = param => Response.AsJson(new { hello = "world v2" });
        }
    }

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
