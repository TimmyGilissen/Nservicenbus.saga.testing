﻿using System.Collections.Generic;
using System.Linq;
using NServiceBus.Pipeline;

namespace Nservicenbus.saga.testing.Tests.Infrastructure
{
    public class ObservedMessageContexts
    {
        public IReadOnlyList<IIncomingLogicalMessageContext> IncomingMessageContexts { get; }
        public IReadOnlyList<IOutgoingLogicalMessageContext> OutgoingMessageContexts { get; }
        public IReadOnlyList<IInvokeHandlerContext> InvokeHandlerContexts { get; }

        public IEnumerable<object> SentMessages => OutgoingMessageContexts.Select(c => c.Message.Instance);
        public IEnumerable<object> ReceivedMessages => IncomingMessageContexts.Select(c => c.Message.Instance);
        public IEnumerable<object> InvokedHandlers => InvokeHandlerContexts.Select(c => c);

        public ObservedMessageContexts(
            IReadOnlyList<IIncomingLogicalMessageContext> incomingMessageContexts,
            IReadOnlyList<IOutgoingLogicalMessageContext> outgoingMessageContexts,
            IReadOnlyList<IInvokeHandlerContext> invokeHandlerContexts
            )
        {
            IncomingMessageContexts = incomingMessageContexts;
            OutgoingMessageContexts = outgoingMessageContexts;
            InvokeHandlerContexts = invokeHandlerContexts;
        }
    }
}