using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace InheritBDD.xUnit
{
    public class ConsoleOutputVisitor : TestMessageVisitor
    {
        private readonly object @lock;
        private int identationLevel;

        public ConsoleOutputVisitor()
        {
            @lock = new object();
            identationLevel = 0;
        }

        private void OutputLine(string msg)
        {
            lock (@lock)
            {
                Console.Write(new string(' ', identationLevel * 4));
                Console.WriteLine(msg);
            }
        }

        //protected override bool Visit(ITestClassStarting testClassStarting)
        //{
        //    OutputLine(testClassStarting.TestClass.Class.Name);
        //    identationLevel++;
        //    return true;
        //}

        //protected override bool Visit(ITestClassFinished testClassFinished)
        //{
        //    identationLevel--;
        //    return base.Visit(testClassFinished);
        //}

        //protected override bool Visit(IDiagnosticMessage diagnosticMessage)
        //{
        //    OutputLine(diagnosticMessage.Message);
        //    return true;
        //}

        //protected override bool Visit(IErrorMessage error)
        //{
        //    foreach (var msg in error.Messages)
        //        OutputLine(msg);
        //    return true;
        //}

        protected override bool Visit(ITestFinished testFinished)
        {
            var name = testFinished.Test.DisplayName;

            OutputLine(name);

            return true;
        }
    }
}