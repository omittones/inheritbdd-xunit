using BDD.Framework;
using Xunit;

[assembly: TestFramework("BDD.Framework.CustomFramework", "BDD.Framework")]

namespace BDD.Debugger
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return StaticRunner.RunAll(typeof (Given_one).Assembly);
        }
    }
}