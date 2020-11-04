using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Extensions.IntegrationTesting;

namespace Nservicenbus.saga.testing.Tests.Infrastructure
{
    public class TestFactory<T> : WebApplicationFactory<T> where T: class
    {
        protected override IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
            .UseNServiceBus(ctxt =>
            {
                var endpoint = new EndpointConfiguration("HostApplicationFactoryTests");
                endpoint.ConfigureTestEndpoint();
                endpoint.UsePersistence<LearningPersistence>();
                endpoint.Pipeline.Register(new InvokedHandlerDiagnostics(), "Raises diagnostic events when a handler/saga was invoked.");
                return endpoint;
            })
            .ConfigureWebHostDefaults(b => { b.Configure(app => { });})
        ;
    }
}