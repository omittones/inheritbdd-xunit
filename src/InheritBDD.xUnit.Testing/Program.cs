using Xunit;

[assembly: TestFramework("InheritBDD.xUnit.CustomFramework", "InheritBDD.xUnit")]

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