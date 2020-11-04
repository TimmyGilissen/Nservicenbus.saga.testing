using System;
using System.Threading.Tasks;
using NServiceBus;
using NserviceBus.Saga.Testing.Commands;
using NserviceBus.Saga.Testing.Events;

namespace NserviceBus.Saga.Testing.Application.Saga
{
    public class JustASaga : Saga<JustASagaData>,
        IAmStartedByMessages<FirstCommand>,
        IHandleMessages<SecondCommandHandled>,
        IHandleMessages<ThirdCommandHandled>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<JustASagaData> mapper)
        {
            mapper.ConfigureMapping<FirstCommand>(m => m.Id).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<SecondCommandHandled>(m => m.SagaId).ToSaga(s => s.SagaId);
            mapper.ConfigureMapping<ThirdCommandHandled>(m => m.SagaId).ToSaga(s => s.SagaId);
        }

        public async Task Handle(FirstCommand message, IMessageHandlerContext context)
        {
            Data.SagaId = message.Id;
            await context.SendLocal(new SecondCommand
            {
                SagaId = message.Id
            });

            await context.SendLocal(new ThirdCommand
            {
                SagaId = message.Id
            });
        }

        public Task Handle(SecondCommandHandled message, IMessageHandlerContext context)
        {
            Data.SecondMessageIsProcessed = true;
            FinishSaga();
            return Task.CompletedTask;
        }

        public Task Handle(ThirdCommandHandled message, IMessageHandlerContext context)
        {
            Data.ThirdMessageIsProcessed = true;
            FinishSaga();
            return Task.CompletedTask;
        }

        private void FinishSaga()
        {
            if (Data.SecondMessageIsProcessed && Data.ThirdMessageIsProcessed)
            {
                MarkAsComplete();
            }
        }
    }

    public class JustASagaData : ContainSagaData
    {
        public Guid SagaId { get; set; }
        public bool SecondMessageIsProcessed { get; set; }
        public bool ThirdMessageIsProcessed { get; set; }
    }
}