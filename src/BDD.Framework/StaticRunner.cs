using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Sdk;

[assembly: TestFramework("BDD.Framework.CustomFramework", "BDD.Framework")]

namespace BDD.Framework
{
    public static class StaticRunner
    {
        public static int RunAll(Assembly assembly)
        {
            var logger = new ConsoleOutputVisitor();
            var xassembly = new ReflectionAssemblyInfo(assembly);

            XunitFrontController controller = new XunitFrontController(xassembly.AssemblyPath, shadowCopy: false);

            controller.Find(false, logger, new NullOptions());

            controller.RunAll(logger, new NullOptions(), new NullOptions());

            return 0;
        }
    }
}