using System;
using NServiceBus;

namespace NserviceBus.Saga.Testing.Commands
{
    public class ThirdCommand : ICommand
    {
        public Guid SagaId { get; set; }
    }
}