using System.Reflection;
using InheritBDD.xUnit;
using Xunit;

[assembly: TestFramework(Framework.TypeName, Framework.AssemblyName)]

namespace InheritBDD.xUnit.Nuget
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SelfHostedRunner.RunAll(Assembly.GetExecutingAssembly());
        }
    }
}
