using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using WebApp.RateLimiting;

namespace WebApp
{
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        private readonly InMemorySingleInstanceRecentCallLogStore _callLog;

        public Bootstrapper()
        {
            _callLog = new InMemorySingleInstanceRecentCallLogStore();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.BeforeRequest += ctx => new RateLimitingPipeline(_callLog).RateLimitRequests(ctx, container);

            base.ApplicationStartup(container, pipelines);
        }
    }
}
