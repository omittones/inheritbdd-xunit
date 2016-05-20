using System.Reflection;
using InheritBDD.xUnit;

namespace InheritBDD.Nuget
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SelfHostedRunner.RunAll(Assembly.GetExecutingAssembly());
        }
    }
}
