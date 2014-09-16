using Nancy;
using WebApp.Infrastructure.Versioning;

namespace WebApp.Api.Resource1
{
    public class Resource1V2 : NancyModule
    {
        public Resource1V2()
            : base("/resource1")
        {
            Get["/", c => c.ForVersion("2")] = param => Response.AsJson(new { hello = "world v2" });
        }
    }
}