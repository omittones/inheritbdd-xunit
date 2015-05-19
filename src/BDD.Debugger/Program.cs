using BDD.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace BDD.Debugger
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return BDD.Framework.Program.Main(args);
        }
    }
}