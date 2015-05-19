using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using TestMethodDisplay = Xunit.Sdk.TestMethodDisplay;

namespace Perform.EvaluationRating.Tests.Infrastructure
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
                    var wrapped = new BDDNameTestCaseDecorator(testCase);
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
                        var wrapped = new BDDNameTestCaseDecorator(testCase);
                        ReportDiscoveredTestCase(wrapped, includeSourceInformation, messageBus);
                    }
                }
            }

            return true;
        }
    }

    public class BDDNameTestCaseDecorator : TestMethodTestCase, IXunitTestCase
    {
        private readonly IXunitTestCase inner;

        public BDDNameTestCaseDecorator(IXunitTestCase inner) : base(
            TestMethodDisplay.ClassAndMethod,
            inner.TestMethod,
            inner.TestMethodArguments)
        {
            this.inner = inner;
        }

        protected override void Initialize()
        {
            base.Initialize();

            var prefix = this.inner.TestMethod.TestClass.Class.Name;
            prefix = (prefix ?? "").Split('.').Last();
            var postfix = this.inner.TestMethod.Method.Name
                .ToLower();

            this.DisplayName = String.Format("{0} {1}", prefix, postfix).Replace('_', ' ');

            this.Method = this.inner.Method;
            this.SkipReason = this.inner.SkipReason;
            this.SourceInformation = this.inner.SourceInformation;
            this.TestMethod = this.inner.TestMethod;
            this.TestMethodArguments = this.inner.TestMethodArguments;
            this.Traits = this.inner.Traits;
        }

        protected override string GetUniqueID()
        {
            return this.inner.UniqueID;
        }

        public Task<RunSummary> RunAsync(
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            var t1 = this.inner as ExecutionErrorTestCase;
            if (t1 != null)
            {
                var runner = new ExecutionErrorTestCaseRunner(t1, messageBus, aggregator, cancellationTokenSource);
                return runner.RunAsync();
            }
            else
            {
                var t2 = this.inner as XunitTheoryTestCase;
                if (t2 != null)
                {
                    return new XunitTheoryTestCaseRunner(this,
                        this.DisplayName, this.SkipReason, constructorArguments,
                        diagnosticMessageSink, messageBus, aggregator, cancellationTokenSource)
                        .RunAsync();
                }
                else
                {
                    var t3 = this.inner as XunitTestCase;
                    if (t3 != null)
                    {
                        return new XunitTestCaseRunner(this,
                            this.DisplayName, this.SkipReason, constructorArguments,
                            this.TestMethodArguments, messageBus, aggregator, cancellationTokenSource)
                            .RunAsync();
                    }
                    else
                    {
                        throw new Exception(this.inner.GetType().Name + " is unkown testcase type!");
                    }
                }
            }
        }
    }
}