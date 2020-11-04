using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NServiceBus.Extensions.Diagnostics;
using NServiceBus.Pipeline;

namespace Nservicenbus.saga.testing.Tests.Infrastructure
{
    public class InvokedHandlerDiagnostics: Behavior<IInvokeHandlerContext>
    {
        private readonly DiagnosticListener _diagnosticListener;
        private const string EventName ="NServiceBus.Extensions.Diagnostics.InvokedHandler.Processed";

        public InvokedHandlerDiagnostics(DiagnosticListener diagnosticListener)
        {
            _diagnosticListener = diagnosticListener;
        }

        public InvokedHandlerDiagnostics() : this(new DiagnosticListener("NServiceBus.Extensions.Diagnostics.InvokedHandler"))
        {
        }

        public override async Task Invoke(IInvokeHandlerContext context, Func<Task> next)
        {
            await next().ConfigureAwait(false);

            if (_diagnosticListener.IsEnabled(EventName))
            {
                _diagnosticListener.Write(EventName, context);
            }
        }
    }
}