using Nancy;
using WebApp.Infrastructure.Versioning;

namespace WebApp.Api.Method1
{
    public class Method1V2 : NancyModule
    {
        public Method1V2()
            : base("/method1")
        {
            Get["/", c => c.ForVersion("2")] = param => Response.AsJson(new { hello = "world v2" });
        }
    }
}