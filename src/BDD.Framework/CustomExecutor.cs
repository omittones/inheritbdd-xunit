using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework
{
    public class CustomExecutor : XunitTestFrameworkExecutor
    {
        public CustomExecutor(
            AssemblyName assemblyName,
            ISourceInformationProvider sourceInformationProvider,
            IMessageSink diagnosticMessageSink) : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
        {
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer()
        {
            return new CustomDiscoverer(this.AssemblyInfo, this.SourceInformationProvider, this.DiagnosticMessageSink);
        }
    }
}