using System;
using Nancy;

namespace WebApp.Api.ResourceWithConneg
{
    public class ResourceWithConneg : NancyModule
    {
        public ResourceWithConneg()
            : base("/resource-with-conneg")
        {
            Get["/"] = param =>
            {
                return new ResourceModelThatSupportsXmlSerialization { hello = "world with conneg" };
            };
        }
    }

    [Serializable]
    public class ResourceModelThatSupportsXmlSerialization
    {
        public string hello { get; set; }
    }
}