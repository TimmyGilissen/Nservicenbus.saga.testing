using System;
using NServiceBus;

namespace NserviceBus.Saga.Testing.Events
{
    public interface ThirdCommandHandled : IEvent
    {
        Guid SagaId { get; set; }
    }
}