using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Pipeline;
using NserviceBus.Saga.Testing.Application.Handlers;
using NserviceBus.Saga.Testing.Application.Saga;
using NserviceBus.Saga.Testing.Commands;
using Nservicenbus.saga.testing.Tests.Infrastructure;
using Xunit;
using static Nservicenbus.saga.testing.Tests.Infrastructure.EndpointFixture;


namespace Nservicenbus.saga.testing.Tests
{
    
    public class JustASagaShould : IClassFixture< TestFactory<JustASagaShould>>
    {
        private readonly TestFactory<JustASagaShould> _factory;

        public JustASagaShould(TestFactory<JustASagaShould> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SimplyComplete()
        {
            var service = _factory.Services.GetService<IMessageSession>();

            var result = await ExecuteAndWaitForSagaCompletion<JustASaga>(
                () => service.SendLocal(new FirstCommand()));

            Assert.Contains(result.InvokedHandlers,  x => ((IInvokeHandlerContext)x).MessageHandler.HandlerType == typeof(CommandHandler));
        }
    }
}