using System;
using NServiceBus;

namespace NserviceBus.Saga.Testing.Commands
{
    public class FirstCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}