using System;
using NServiceBus;

namespace NserviceBus.Saga.Testing.Commands
{
    public class SecondCommand : ICommand
    {
        public Guid SagaId { get; set; }
    }
}