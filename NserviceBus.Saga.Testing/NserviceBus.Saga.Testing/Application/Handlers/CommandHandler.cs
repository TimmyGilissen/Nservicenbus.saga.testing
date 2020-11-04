using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using NserviceBus.Saga.Testing.Commands;
using NserviceBus.Saga.Testing.Events;

namespace NserviceBus.Saga.Testing.Application.Handlers
{
    public class CommandHandler: IHandleMessages<SecondCommand>, IHandleMessages<ThirdCommand>
    {
        public async Task Handle(SecondCommand message, IMessageHandlerContext context)
        {
            var random = new Random();
            var mseconds=random.Next(1, 3) * 1000;   
            Thread.Sleep(mseconds);
            await context.Publish<SecondCommandHandled>(t => t.SagaId = message.SagaId);
        }

        public async Task Handle(ThirdCommand message, IMessageHandlerContext context)
        {
            var random = new Random();
            var mseconds=random.Next(1, 3) * 1000;   
            Thread.Sleep(mseconds);
            await context.Publish<ThirdCommandHandled>(t => t.SagaId = message.SagaId);
        }
    }
}