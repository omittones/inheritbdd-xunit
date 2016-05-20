using InheritBDD.xUnit;
using Xunit;

[assembly: TestFramework(Framework.TypeName, Framework.AssemblyName)]

namespace InheritBDD.xUnit.Testing
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return SelfHostedRunner.RunAll(typeof (Given_one).Assembly);
        }
    }
}