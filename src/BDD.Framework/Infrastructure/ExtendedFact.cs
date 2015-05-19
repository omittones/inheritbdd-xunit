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

            this.DisplayName = TransformName(this, this.MethodGenericTypes);
        }

        public static string TransformName(IXunitTestCase testCase, ITypeInfo[] genericTypes)
        {
            var prefix = testCase.TestMethod.TestClass.Class.Name;
            prefix = (prefix ?? "").Split('.').Last();
            var postfix = testCase.TestMethod.Method.Name
                .ToLower();

            var name = String.Format("{0} {1}", prefix, postfix).Replace('_', ' ');

            var method = testCase.TestMethod;
            var args = testCase.TestMethodArguments;
            if (args != null && args.Any())
            {
                name = method.Method.GetDisplayNameWithArguments(testCase.DisplayName, args, genericTypes);
            }

            return name;
        }
    }
}