using Nancy;
using WebApp.Infrastructure.Versioning;

namespace WebApp.Api.Resource1
{
    public class Resource1V1 : NancyModule
    {
        public Resource1V1()
            : base("/resource1")
        {
            Get["/", c => c.ForVersion("1")] = param => Response.AsJson(new { hello = "world v1" });
        }
    }
}