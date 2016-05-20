using System.Reflection;
using InheritBDD.xUnit;
using Xunit;
using Xunit.Sdk;

namespace InheritBDD.xUnit
{
    public static class SelfHostedRunner
    {
        static SelfHostedRunner()
        {
            MaxThreadCount = 1;
        }

        public static int MaxThreadCount { get; set; }

        public static int RunAll(Assembly assembly)
        {
            var logger = new ConsoleOutputVisitor();

            var xassembly = new ReflectionAssemblyInfo(assembly);

            var discoveryOptions = TestFrameworkOptions.ForDiscovery();
            discoveryOptions.SetSynchronousMessageReporting(true);
            discoveryOptions.SetDiagnosticMessages(true);

            var executionOptions = TestFrameworkOptions.ForExecution();
            executionOptions.SetSynchronousMessageReporting(true);
            executionOptions.SetDiagnosticMessages(true);
            executionOptions.SetMaxParallelThreads(MaxThreadCount);
            executionOptions.SetDisableParallelization(MaxThreadCount <= 1);

            var controller = new XunitFrontController(
                xassembly.AssemblyPath,
                shadowCopy: false,
                diagnosticMessageSink: logger);

            controller.RunAll(logger, discoveryOptions, executionOptions);

            return 0;
        }
    }
}