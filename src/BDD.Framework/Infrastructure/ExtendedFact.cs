using System;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework.Infrastructure
{
    public class ExtendedFact : XunitTestCase, IXunitTestCase
    {
        [Obsolete("Called by the deserializer", true)]
        public ExtendedFact()
        {
        }

        public ExtendedFact(IMessageSink diagnosticMessageSink, IXunitTestCase inner)
            : base(diagnosticMessageSink,
                TestMethodDisplay.ClassAndMethod,
                inner.TestMethod,
                inner.TestMethodArguments)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.DisplayName = TransformName(this.TestMethod, this.TestMethodArguments, this.MethodGenericTypes);
        }

        public static string TransformName(
            ITestMethod method,
            object[] methodArgs,
            ITypeInfo[] genericTypes)
        {
            var prefix = method.TestClass.Class.Name;
            prefix = (prefix ?? "").Split('.').Last();
            var postfix = method.Method.Name
                .ToLower();

            var name = String.Format("{0} {1}", prefix, postfix).Replace('_', ' ');

            if (methodArgs != null && methodArgs.Any())
            {
                postfix = method.Method.GetDisplayNameWithArguments("", methodArgs, genericTypes);
                name += " " + postfix;
            }

            return name;
        }
    }
}