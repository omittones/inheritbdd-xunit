using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework.Infrastructure
{
    public class CustomDiscoverer : XunitTestFrameworkDiscoverer
    {
        private HashSet<string> tests;

        public CustomDiscoverer(IAssemblyInfo assemblyInfo,
            ISourceInformationProvider sourceProvider,
            IMessageSink diagnosticMessageSink,
            IXunitTestCollectionFactory collectionFactory = null) : base(assemblyInfo, sourceProvider, diagnosticMessageSink, collectionFactory)
        {
            tests = new HashSet<string>();
        }

        protected virtual void ReportTestCaseOnce(IXunitTestCase test, bool includeSourceInformation, IMessageBus messageBus)
        {
            if (!tests.Contains(test.UniqueID))
            {
                tests.Add(test.UniqueID);
                ReportDiscoveredTestCase(test, includeSourceInformation, messageBus);    
            }
        }

        protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation, IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            foreach (var method in testClass.Class.GetMethods(includePrivateMethods: true))
            {
                var classType = testClass.Class.ToRuntimeType();
                var methodDeclaringType = method.ToRuntimeMethod().DeclaringType;
                if (classType == methodDeclaringType)
                {
                    var testMethod = new TestMethod(testClass, method);
                    if (!FindTestsForMethod(testMethod, includeSourceInformation, messageBus, discoveryOptions))
                        return false;
                }
            }

            return true;
        }

        protected override bool FindTestsForMethod(ITestMethod testMethod,
            bool includeSourceInformation,
            IMessageBus messageBus,
            ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            var factDiscoverer = new ExtendedDiscoverer(new FactDiscoverer(this.DiagnosticMessageSink))
            {
                DiagnosticMessageSink = this.DiagnosticMessageSink
            };
            var theoryDiscoverer = new ExtendedDiscoverer(new TheoryDiscoverer(this.DiagnosticMessageSink))
            {
                DiagnosticMessageSink = this.DiagnosticMessageSink
            };
            
            var theory = testMethod.Method.GetCustomAttributes(typeof (TheoryAttribute)).FirstOrDefault();
            if (theory != null)
            {
                foreach (var test in theoryDiscoverer.Discover(discoveryOptions, testMethod, theory))
                    ReportTestCaseOnce(test, includeSourceInformation, messageBus);
            }
            else
            {
                var fact = testMethod.Method.GetCustomAttributes(typeof (FactAttribute)).FirstOrDefault();
                if (fact != null)
                {
                    foreach (var test in factDiscoverer.Discover(discoveryOptions, testMethod, fact))
                        ReportTestCaseOnce(test, includeSourceInformation, messageBus);
                }
            }

            return true;
        }
    }
}