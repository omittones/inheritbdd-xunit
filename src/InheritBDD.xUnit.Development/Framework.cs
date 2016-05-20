using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace InheritBDD.xUnit
{
    public class Framework : XunitTestFramework
    {
        public const string AssemblyName = "InheritBdd.xUnit";

        public const string TypeName = "InheritBdd.xUnit.Framework";

        public Framework(IMessageSink messageSink) : base(messageSink)
        {
            this.SourceInformationProvider = new NullSourceInformationProvider();
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo)
        {
            return new CustomDiscoverer(assemblyInfo, this.SourceInformationProvider, this.DiagnosticMessageSink);
        }

        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
        {
            return new CustomExecutor(assemblyName, this.SourceInformationProvider, this.DiagnosticMessageSink);
        }
    }
}