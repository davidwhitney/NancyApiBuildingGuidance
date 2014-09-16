using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy;

namespace WebApp.Infrastructure.AutoDoc
{
    public class DocumentationModule : NancyModule
    {
        public static Func<Type, NancyModule> CreateModuleFunc { get; set; }

        public DocumentationModule():base("/docs")
        {
            Get["/"] = x => DiscoverAllApiRoutes();
        }

        private List<object> DiscoverAllApiRoutes()
        {
            var routeDescriptions =
                new List<object>(
                    Routes.Select(item => new { Method = item.Description.Method, Uri = item.Description.Path }).ToList());

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(NancyModule)) && x != typeof(DocumentationModule)))
            {
                var nancyModule = CreateModuleFunc(type);
                var routesOnThis =
                    nancyModule.Routes.Select(item => new { Method = item.Description.Method, Uri = item.Description.Path })
                        .ToList();
                routeDescriptions.AddRange(routesOnThis);
            }

            return routeDescriptions;
        }
    }
}
