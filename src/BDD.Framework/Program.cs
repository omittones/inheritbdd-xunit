using System.Reflection;
using System.Text;
using BDD.Framework.Infrastructure;
using Xunit;
using Xunit.Sdk;

[assembly: TestFramework("BDD.Framework.Infrastructure.CustomFramework", "BDD.Framework")]

namespace BDD.Framework
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var logger = new ConsoleOutputVisitor();
            var assembly = new ReflectionAssemblyInfo(typeof (Program).Assembly);

            XunitFrontController controller = new XunitFrontController(assembly.AssemblyPath, shadowCopy: false);

            controller.Find(false, logger, new NullOptions());

            controller.RunAll(logger, new NullOptions(), new NullOptions());
            
            return 0;
        }
    }
}