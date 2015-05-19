using System.Reflection;
using System.Text;
using Perform.EvaluationRating.Tests.Infrastructure;
using Xunit;
using Xunit.Sdk;

[assembly: TestFramework("Perform.EvaluationRating.Tests.Infrastructure.CustomFramework", "Perform.EvaluationRating.Tests")]

namespace Perform.EvaluationRating.Tests
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var logger = new ConsoleOutputVisitor();
            var assembly = new ReflectionAssemblyInfo(Assembly.GetExecutingAssembly());

            //var proxy = new TestFrameworkProxy(
            //    assembly,
            //    new NullSourceInformationProvider(),
            //    logger);

            //var executor = proxy.GetExecutor(assembly.Assembly.GetName());

            //executor.RunAll(logger,
            //    new NullOptions(),
            //    new NullOptions());

            XunitFrontController controller = new XunitFrontController(assembly.AssemblyPath, shadowCopy: true);
            controller.RunAll(logger, new NullOptions(), new NullOptions());

            return 0;
        }
    }
}