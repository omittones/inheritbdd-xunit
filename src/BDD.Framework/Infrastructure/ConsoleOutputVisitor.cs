using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Perform.EvaluationRating.Tests.Infrastructure
{
    public class ConsoleOutputVisitor : TestMessageVisitor
    {
        protected override bool Visit(IDiagnosticMessage diagnosticMessage)
        {
            Console.WriteLine(diagnosticMessage.Message);
            return true;
        }

        protected override bool Visit(IErrorMessage error)
        {
            foreach (var msg in error.Messages)
                Console.WriteLine(msg);
            return true;
        }

        protected override bool Visit(ITestFinished testFinished)
        {
            Console.WriteLine(testFinished.Test.DisplayName);
            return true;
        }
    }
}