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

        private static string FirstLowercase(string name)
        {
            if (name.Length > 1)
                return name.Substring(0, 1).ToLower() + name.Substring(1);
            else
                return name.ToLower();
        }

        private static string[] ExtractTypeNames(ITestMethod method)
        {
            var prefix = method.TestClass.Class.Name;
            prefix = (prefix ?? "").Split('.').Last();
            return prefix.Split('+');
        }

        private static string TransformName(
            ITestMethod method,
            object[] methodArgs,
            ITypeInfo[] genericTypes)
        {
            var names = ExtractTypeNames(method);
            var prefix = names.First();
            for (int i = 1; i < names.Length; i++)
                prefix += " " + FirstLowercase(names[i]);

            var postfix = FirstLowercase(method.Method.Name);

            var fullName = String.Format("{0}, {1}", prefix, postfix).Replace('_', ' ');

            if (methodArgs != null && methodArgs.Any())
            {
                postfix = method.Method.GetDisplayNameWithArguments("", methodArgs, genericTypes);
                fullName += " " + postfix;
            }

            return fullName;
        }
    }
}