using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Perform.EvaluationRating.Tests.Infrastructure
{
    public class CustomFramework : XunitTestFramework
    {
        public CustomFramework(IMessageSink messageSink) : base(messageSink)
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