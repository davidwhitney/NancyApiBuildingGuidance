using Nancy;
using WebApp.Versioning;

namespace WebApp.Api.Method1
{
    public class Method1V1 : NancyModule
    {
        public Method1V1()
            : base("/method1")
        {
            Get["/", c => c.ForVersion("1")] = param => Response.AsJson(new { hello = "world v1" });
        }
    }
}