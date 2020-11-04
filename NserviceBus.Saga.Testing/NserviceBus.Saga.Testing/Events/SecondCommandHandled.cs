using System;
using NServiceBus;

namespace NserviceBus.Saga.Testing.Events
{
    public interface SecondCommandHandled : IEvent
    {
        Guid SagaId { get; set; }
    }
}