using Nancy;
using WebApp.Infrastructure.Versioning;

namespace WebApp.Api.ResourceWithManyVersionsInOneModule
{
    public class SomeMethodWithVersionsInOneModule : NancyModule
    {
        public SomeMethodWithVersionsInOneModule()
            : base("/resource-with-many-versions-in-one-module")
        {
            Get["/"] = param => Response.AsJson(new { hello = "world v-unknown" });
            Get["/", c => c.ForVersion("1")] = param => Response.AsJson(new { hello = "world v1" });
            Get["/", c => c.ForVersion("2")] = param => Response.AsJson(new { hello = "world v2" });
        }
    }
}