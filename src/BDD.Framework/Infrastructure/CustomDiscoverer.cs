using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework.Infrastructure
{
    public class CustomDiscoverer : XunitTestFrameworkDiscoverer
    {
        public CustomDiscoverer(IAssemblyInfo assemblyInfo,
            ISourceInformationProvider sourceProvider,
            IMessageSink diagnosticMessageSink,
            IXunitTestCollectionFactory collectionFactory = null) : base(assemblyInfo, sourceProvider, diagnosticMessageSink, collectionFactory)
        {
        }

        protected override bool FindTestsForMethod(ITestMethod testMethod,
            bool includeSourceInformation,
            IMessageBus messageBus,
            ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            var factDiscoverer = new FactDiscoverer(this.DiagnosticMessageSink);
            var theoryDiscoverer = new TheoryDiscoverer(this.DiagnosticMessageSink);

            var theory = testMethod.Method.GetCustomAttributes(typeof (TheoryAttribute)).FirstOrDefault();
            if (theory != null)
            {
                foreach (var testCase in theoryDiscoverer.Discover(discoveryOptions, testMethod, theory))
                {
                    var wrapped = new ExtendedTheory(this.DiagnosticMessageSink, testCase);
                    ReportDiscoveredTestCase(wrapped, includeSourceInformation, messageBus);
                }
            }
            else
            {
                var fact = testMethod.Method.GetCustomAttributes(typeof (FactAttribute)).FirstOrDefault();
                if (fact != null)
                {
                    foreach (var testCase in factDiscoverer.Discover(discoveryOptions, testMethod, fact))
                    {
                        var wrapped = new ExtendedFact(this.DiagnosticMessageSink, testCase);
                        ReportDiscoveredTestCase(wrapped, includeSourceInformation, messageBus);
                    }
                }
            }

            return true;
        }
    }
}