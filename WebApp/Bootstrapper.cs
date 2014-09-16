using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using WebApp.Infrastructure.RateLimiting;
using WebApp.Infrastructure.RateLimiting.RateLimitingStorage;

namespace WebApp
{
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        private readonly RateLimitingPipeline _rateLimiter;

        public Bootstrapper()
        {
            _rateLimiter = new RateLimitingPipeline(new InMemorySingleInstanceRecentCallLogStore());
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.BeforeRequest += _rateLimiter.RateLimitRequests;

            base.ApplicationStartup(container, pipelines);
        }
    }
}
